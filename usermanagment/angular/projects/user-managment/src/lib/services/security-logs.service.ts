import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';
import { Observable } from 'rxjs';
import { SecurityLogDto, GetSecurityLogInput } from '../models/security-log.model';

@Injectable({ providedIn: 'root' })
export class SecurityLogsService {
  apiName = 'UserManagment';

  constructor(private restService: RestService) {}

  getList(input: GetSecurityLogInput): Observable<{ items: SecurityLogDto[]; totalCount: number }> {
    // Remove empty string and undefined fields
    const params: any = { ...input };
    Object.keys(params).forEach(
      key => (params[key] === '' || params[key] === undefined) && delete params[key]
    );

    return this.restService.request({
      method: 'GET',
      url: '/api/user-managment/security-logs',
      params,
    }, { apiName: this.apiName });
  }
} 