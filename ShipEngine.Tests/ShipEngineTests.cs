using ShipEngineSDK;
using ShipEngineSDK.CreateLabelFromShipmentDetails.Params;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Xunit;

namespace ShipEngineTest
{
    public class ShipEngineTests
    {

        [Fact]
        public void NullAPIKeyStringThrowsError()
        {
            var ex = Assert.Throws<ShipEngineException>(() => new ShipEngine(apiKey: null));
            Assert.Equal(ErrorSource.ShipEngine, ex.ErrorSource);
            Assert.Equal(ErrorType.Validation, ex.ErrorType);
            Assert.Equal(ErrorCode.FieldValueRequired, ex.ErrorCode);
            Assert.Equal("A ShipEngine API key must be specified", ex.Message);
            Assert.Null(ex.RequestId);
        }

        [Fact]
        public void EmptyAPIKeyStringThrowsError()
        {
            var ex = Assert.Throws<ShipEngineException>(() => new ShipEngine(""));
            Assert.Equal(ErrorSource.ShipEngine, ex.ErrorSource);
            Assert.Equal(ErrorType.Validation, ex.ErrorType);
            Assert.Equal(ErrorCode.FieldValueRequired, ex.ErrorCode);
            Assert.Equal("A ShipEngine API key must be specified", ex.Message);
            Assert.Null(ex.RequestId);
        }

        [Fact]
        public void InvalidsTimeoutAtInstantiation()
        {
            var ex = Assert.Throws<ShipEngineException>(
                () => new ShipEngine(
                    new Config(apiKey: "TEST_1234", timeout: System.TimeSpan.FromSeconds(-1))
                )
            );
            Assert.Equal(ErrorSource.ShipEngine, ex.ErrorSource);
            Assert.Equal(ErrorType.Validation, ex.ErrorType);
            Assert.Equal(ErrorCode.InvalidFieldValue, ex.ErrorCode);
            Assert.Equal("Timeout must be greater than zero.", ex.Message);
            Assert.Null(ex.RequestId);
        }
    }
}