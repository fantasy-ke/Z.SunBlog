using Microsoft.AspNetCore.Mvc;
using Z.Ddd.Common.Authorization;

namespace Z.NetWiki.Host.Controllers
{

    /// <summary>
    /// ������
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
        /// ��ȡ����
        /// </summary>
        /// <returns>��ϸ����</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            UserTokenModel tokenModel = new UserTokenModel();
            tokenModel.UserName = "test";
            tokenModel.UserId =Guid.NewGuid().ToString("N");
            var dfs = _jwtTokenProvider.GenerateAccessToken(tokenModel);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}