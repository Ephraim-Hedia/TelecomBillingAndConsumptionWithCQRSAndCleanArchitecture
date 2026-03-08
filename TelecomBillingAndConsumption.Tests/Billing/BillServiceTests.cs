using MockQueryable;
using Moq;
using TelecomBillingAndConsumption.Data.Entities;
using TelecomBillingAndConsumption.Data.Helpers;
using TelecomBillingAndConsumption.Infrastructure.Interfaces;
using TelecomBillingAndConsumption.Service.HelperDtos;
using TelecomBillingAndConsumption.Service.Implementation;
using TelecomBillingAndConsumption.Service.Interfaces;
using TelecomBillingAndConsumption.Service.Interfaces.PlanService;

namespace TelecomBillingAndConsumption.Tests.Billing
{
    public class BillServiceTests
    {
        private readonly Mock<ISubscriberService> _subscriberService = new();
        private readonly Mock<IPlanService> _planService = new();
        private readonly Mock<IUsageSummaryService> _usageSummaryService = new();
        private readonly Mock<IBillRepository> _billRepository = new();
        private readonly Mock<ITariffCacheService> _tariffCache = new();

        private BillService CreateService()
        {
            return new BillService(
                _subscriberService.Object,
                _planService.Object,
                _usageSummaryService.Object,
                _billRepository.Object,
                _tariffCache.Object
            );
        }

        private void SetupTariff(UsageType type, bool roaming, bool peak, decimal price)
        {
            _tariffCache
                .Setup(x => x.TryGetPrice(type, roaming, peak, out It.Ref<decimal>.IsAny))
                .Returns((UsageType t, bool r, bool p, out decimal result) =>
                {
                    result = price;
                    return true;
                });
        }

        private void SetupTariffs()
        {
            SetupTariff(UsageType.Call, false, true, 0.15m);
            SetupTariff(UsageType.Call, false, false, 0.05m);
            SetupTariff(UsageType.Data, false, false, 0.05m);
            SetupTariff(UsageType.SMS, false, false, 0.02m);
        }

        [Fact]
        public async Task GenerateMonthlyBill_ShouldApplyExtraUsage_WhenBundleExceeded()
        {
            // Arrange
            var service = CreateService();

            var subscriber = new Subscriber
            {
                Id = 1,
                PlanId = 1,
                IsRoaming = false,
                CreatedAt = DateTime.UtcNow.AddYears(-3)
            };

            var plan = new Plan
            {
                Id = 1,
                MonthlyFee = 100,
                IncludedCallMinutes = 1000,
                IncludedDataMB = 10000,
                IncludedSMS = 100
            };

            var usage = new SubscriberUsageSummaryResponse
            {
                PeakCalls = 1200,
                OffPeakCalls = 0,
                UsedDataMB = 500,
                UsedSmsCount = 20
            };

            _subscriberService
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(subscriber);

            _planService
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(plan);

            _usageSummaryService
                .Setup(x => x.GetUsageSummaryAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(usage);

            SetupTariffs();

            // IMPORTANT: Mock EF async IQueryable
            var bills = new List<Bill>().AsQueryable().BuildMock();

            _billRepository
                .Setup(x => x.GetBillsBySubscriberIdQuarable(It.IsAny<int>()))
                .Returns(bills);

            // Act
            var billId = await service.GenerateMonthlyBillAsync(1, "2026-03");

            // Assert
            _billRepository.Verify(x =>
                x.AddAsync(It.Is<Bill>(b =>
                    b.ExtraUsageCost > 0 &&
                    b.LoyaltyDiscount > 0 &&
                    b.VatAmount > 0 &&
                    b.TotalAmount > 0
                )),
                Times.Once);
        }
    }
}