import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

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
}
