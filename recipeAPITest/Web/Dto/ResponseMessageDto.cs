using recipeAPITest.TestUtils;
using recipies_ms.Web.Dto;
using Xunit;

namespace recipeAPITest.Web.Dto
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