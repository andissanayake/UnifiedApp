using FluentAssertions;

namespace ApiTest.AppResponse
{
    public class AppResponseTest
    {

        [Fact]
        public void SuccessResponseOne()
        {
            var res = Service.AppResponse<bool>.SuccessResponse(true);
            res.Should().NotBeNull();
            res.IsSucceed.Should().Be(true);
        }
        [Fact]
        public void SuccessResponseTwo()
        {
            var res = Service.AppResponse<bool>.SuccessResponse(true, "key", "message");
            res.Should().NotBeNull();
            res.IsSucceed.Should().Be(true);
            res.Messages.First(item => item.Key == "key").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key").Value.First(item => item == "message").Should().NotBeNull();
        }

        [Fact]
        public void SuccessResponseThree()
        {
            var res = Service.AppResponse<bool>.SuccessResponse(true, "key1", ["message1", "message2"]);
            res.Should().NotBeNull();
            res.IsSucceed.Should().Be(true);
            res.Messages.First(item => item.Key == "key1").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key1").Value.First(item => item == "message1").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key1").Value.First(item => item == "message2").Should().NotBeNull();
        }
        [Fact]
        public void SuccessResponseFour()
        {
            var dic = new Dictionary<string, string[]>
            {
                { "key1", ["message1", "message2"] },
                { "key2", ["message1", "message2"] },

            };
            var res = Service.AppResponse<bool>.SuccessResponse(true, dic);
            res.Should().NotBeNull();
            res.IsSucceed.Should().Be(true);
            res.Messages.First(item => item.Key == "key1").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key1").Value.First(item => item == "message1").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key1").Value.First(item => item == "message2").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key2").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key2").Value.First(item => item == "message1").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key2").Value.First(item => item == "message2").Should().NotBeNull();
        }

        [Fact]
        public void ErrorResponseOne()
        {
            var res = Service.AppResponse<bool>.ErrorResponse("key", "message");
            res.Should().NotBeNull();
            res.IsSucceed.Should().Be(false);
            res.Messages.First(item => item.Key == "key").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key").Value.First(item => item == "message").Should().NotBeNull();
        }

        [Fact]
        public void ErrorResponseTwo()
        {
            var res = Service.AppResponse<bool>.ErrorResponse("key1", ["message1", "message2"]);
            res.Should().NotBeNull();
            res.IsSucceed.Should().Be(false);
            res.Messages.First(item => item.Key == "key1").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key1").Value.First(item => item == "message1").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key1").Value.First(item => item == "message2").Should().NotBeNull();
        }
        [Fact]
        public void ErrorResponseThree()
        {
            var dic = new Dictionary<string, string[]>
            {
                { "key1", ["message1", "message2"] },
                { "key2", ["message1", "message2"] },

            };
            var res = Service.AppResponse<bool>.ErrorResponse(dic);
            res.Should().NotBeNull();
            res.IsSucceed.Should().Be(false);
            res.Messages.First(item => item.Key == "key1").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key1").Value.First(item => item == "message1").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key1").Value.First(item => item == "message2").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key2").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key2").Value.First(item => item == "message1").Should().NotBeNull();
            res.Messages.First(item => item.Key == "key2").Value.First(item => item == "message2").Should().NotBeNull();
        }

    }
}
