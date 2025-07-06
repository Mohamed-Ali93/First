import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

export interface MoveUserToRoleDto {
  userId: string;
  sourceRoleId: string;
  targetRoleId: string;
  userName?: string;
  sourceRoleName?: string;
  targetRoleName?: string;
}

@Injectable({
  providedIn: 'root',
})
export class UserManagmentService {
  apiName = 'UserManagment';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/user-managment/sample' },
      { apiName: this.apiName }
    );
  }

  getRolesWithUserCount() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/user-managment/roles-with-user-count' },
      { apiName: this.apiName }
    );
  }

  // Specific functionality: Move user from one role to another (keeping in both roles)
  moveUserToRole(input: MoveUserToRoleDto) {
    return this.restService.request<MoveUserToRoleDto, void>(
      { method: 'POST', url: '/api/user-managment/roles-with-user-count/move-user-to-role', body: input },
      { apiName: this.apiName }
    );
  }

  // Get users in a specific role
  getUsersInRole(roleId: string) {
    return this.restService.request<void, any>(
      { method: 'GET', url: `/api/user-managment/roles-with-user-count/${roleId}/users` },
      { apiName: this.apiName }
    );
  }

  // Move all users from one role to another (keeping them in both roles)
  moveAllUsersToRole(input: { sourceRoleId: string, targetRoleId: string }) {
    return this.restService.request<any, void>(
      { method: 'POST', url: '/api/user-managment/roles-with-user-count/move-all-users-to-role', body: input },
      { apiName: this.apiName }
    );
  }
}
