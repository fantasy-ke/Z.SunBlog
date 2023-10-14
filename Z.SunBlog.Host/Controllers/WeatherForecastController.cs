using Microsoft.AspNetCore.Mvc;
using Z.Ddd.Common.Authorization;

namespace Z.SunBlog.Host.Controllers
{

    /// <summary>
    /// 天气控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IJwtTokenProvider _jwtTokenProvider;

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IJwtTokenProvider jwtTokenProvider)
        {
            _logger = logger;
            _jwtTokenProvider = jwtTokenProvider;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns>��ϸ����</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
            UserTokenModel tokenModel = new UserTokenModel();
            tokenModel.UserName = "test";
            tokenModel.UserId =Guid.NewGuid().ToString("N");
            var dfs = _jwtTokenProvider.GenerateAccessToken(tokenModel);

            return dfs;
        }
    }
}