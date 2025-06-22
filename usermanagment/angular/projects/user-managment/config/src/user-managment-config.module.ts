import { ModuleWithProviders, NgModule } from '@angular/core';
import { USER_MANAGMENT_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class UserManagmentConfigModule {
  static forRoot(): ModuleWithProviders<UserManagmentConfigModule> {
    return {
      ngModule: UserManagmentConfigModule,
      providers: [USER_MANAGMENT_ROUTE_PROVIDERS],
    };
  }
}
