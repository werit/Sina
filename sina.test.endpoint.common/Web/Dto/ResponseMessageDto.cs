using sina.endpoint.common.Web.Dto;
using sina.test.endpoint.common.TestUtils;
using Xunit;

namespace sina.test.endpoint.common.Web.Dto
{
    public class ResponseMessageDtoTest
    {
        [Trait(TestCategories.TraitName, TestCategories.UnitTest)]
        [Theory]
        [InlineData("some random message")]
        [InlineData("")]
        [InlineData(null)]
        public void ResponseMessageDtoBasicTest(string message)
        {
            var respMessage = new ResponseMessageDto {Message = message};
            Assert.Equal(message, respMessage.Message);
        }
    }
}