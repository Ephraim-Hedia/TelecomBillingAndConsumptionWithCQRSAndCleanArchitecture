using FluentAssertions;
using TelecomBillingAndConsumption.Service.Implementation;

namespace TelecomBillingAndConsumption.Tests.Usage
{
    public class UsageRecordServiceTests
    {
        [Fact]
        public void IsPeakHour_ShouldReturnTrue_WhenTimeIs10AM()
        {
            // Arrange
            var service = new UsageRecordService(null, null);
            var timestamp = new DateTime(2026, 1, 1, 10, 0, 0);

            // Act
            var result = InvokeIsPeakHour(service, timestamp);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsPeakHour_ShouldReturnFalse_WhenTimeIs22PM()
        {
            var service = new UsageRecordService(null, null);
            var timestamp = new DateTime(2026, 1, 1, 22, 0, 0);

            var result = InvokeIsPeakHour(service, timestamp);

            result.Should().BeFalse();
        }

        private bool InvokeIsPeakHour(UsageRecordService service, DateTime timestamp)
        {
            var method = typeof(UsageRecordService)
                .GetMethod("IsPeakHour", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            return (bool)method.Invoke(service, new object[] { timestamp });
        }
    }
}
