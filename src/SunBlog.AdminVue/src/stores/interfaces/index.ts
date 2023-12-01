import { ZFantasyToken, ZUserInfoOutput } from '@/shared/service-proxies';

/* UserState */
export interface UserInfoState {
	zToken: ZFantasyToken;
	userInfo: ZUserInfoOutput;
}
