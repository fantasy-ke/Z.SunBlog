using Microsoft.EntityFrameworkCore;
using MrHuo.OAuth.Gitee;
using MrHuo.OAuth.Github;
using MrHuo.OAuth.QQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Enum;
using Z.Ddd.Common.Entities.Users;
using Z.Ddd.Common.Exceptions;

namespace Z.SunBlog.Core.AuthAccountModule.DomainManager
{
    public class AuthAccountDomainManager : BasicDomainService<AuthAccount, string>, IAuthAccountDomainManager
    {
        public AuthAccountDomainManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(AuthAccount entity)
        {
           await Task.CompletedTask;
        }

        public async Task<AuthAccount> CreateQQAccount(QQUserInfoModel qqInfo,string oauthId)
        {
            var account = await QueryAsNoTracking.FirstAsync(x => x.OAuthId == oauthId && x.Type.ToLower() == "qq");
            var gender = qqInfo.Gender == "男" ? Gender.Male :
                qqInfo.Gender == "女" ? Gender.Female : Gender.Unknown;
            if (account != null)
            {
                await UpdateAsync(new AuthAccount()
                {
                    Avatar = string.IsNullOrWhiteSpace(qqInfo.QQ100Avatar) ? qqInfo.Avatar : qqInfo.QQ100Avatar,
                    Name = qqInfo.Name,
                    Gender = gender
                },
                    x => x.OAuthId == oauthId && x.Type.ToLower() == "qq");
            }
            else
            {
                account = await CreateAsync(new AuthAccount()
                {
                    Gender = gender,
                    Avatar = qqInfo.Avatar,
                    Name = qqInfo.Name,
                    OAuthId = oauthId,
                    Type = "QQ"
                });
            }

            return account;
        }

        public async Task<AuthAccount> CreateGiteeAccount(GiteeUserModel giteeInfo)
        {
            var  account = await QueryAsNoTracking.FirstOrDefaultAsync(x => x.OAuthId == giteeInfo.Name && x.Type.ToLower() == "gitee");
            if (account != null)
            {
                await UpdateAsync(new AuthAccount()
                {
                    Avatar = giteeInfo.Avatar,
                    Name = giteeInfo.Name,
                    Gender = Gender.Unknown
                },
                    x => x.OAuthId == giteeInfo.Name && x.Type.ToLower() == "gitee");
            }
            else
            {
                account = await CreateAsync(new AuthAccount()
                {
                    Gender = Gender.Unknown,
                    Avatar = giteeInfo.Avatar,
                    Name = giteeInfo.Name,
                    OAuthId = giteeInfo.Name,
                    Type = "Gitee"
                });
            }

            return account;
        }

        public async Task<AuthAccount> CreateGitHubAccount(GithubUserModel githubInfo)
        {
            var account = await QueryAsNoTracking.FirstOrDefaultAsync(x => x.OAuthId == githubInfo.Name && x.Type.ToLower() == "github");
            if (account != null)
            {
                await UpdateAsync(new AuthAccount()
                {
                    Avatar = githubInfo.Avatar,
                    Name = githubInfo.Name,
                    Gender = Gender.Unknown
                },
                    x => x.OAuthId == githubInfo.Name && x.Type.ToLower() == "github");
            }
            else
            {
                account = await CreateAsync(new AuthAccount()
                {
                    Gender = Gender.Unknown,
                    Avatar = githubInfo.Avatar,
                    Name = githubInfo.Name,
                    OAuthId = githubInfo.Name,
                    Type = "GitHub"
                });
            }

            return account;
        }
    }
}
