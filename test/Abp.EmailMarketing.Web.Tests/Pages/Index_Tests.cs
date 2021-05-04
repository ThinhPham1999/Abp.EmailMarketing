using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Abp.EmailMarketing.Pages
{
    public class Index_Tests : EmailMarketingWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
