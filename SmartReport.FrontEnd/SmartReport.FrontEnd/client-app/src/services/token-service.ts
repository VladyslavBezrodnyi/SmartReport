import { AppUser } from './../types/common-types';
import jwt_decoded from 'jwt-decode'

const tokenService = (token: string): AppUser => {
    const tokenPayload = jwt_decoded<{ [id: string]: string; }>(token);
    console.log(tokenPayload);
    return {
        id: tokenPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
        roles: tokenPayload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
        userName: tokenPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        access_token: token
    } as AppUser
}

export default tokenService;