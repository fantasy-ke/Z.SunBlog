using MrHuo.OAuth.Gitee;
using MrHuo.OAuth.Github;
using MrHuo.OAuth.QQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Users;

namespace Z.SunBlog.Core.AuthAccountModule.DomainManager
{
    public interface IAuthAccountDomainManager : IBasicDomainService<AuthAccount, string>
    {

        Task<AuthAccount> CreateQQAccount(QQUserInfoModel qqInfo, string oauthId);

        Task<AuthAccount> CreateGiteeAccount(GiteeUserModel giteeInfo);

        Task<AuthAccount> CreateGitHubAccount(GithubUserModel githubInfo);
    }
}
