import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { UserManagmentRoutingModule } from './user-managment-routing.module';
import { eIdentityComponents, IdentityModule } from '@volo/abp.ng.identity';
import { ExtensibleModule } from '@abp/ng.components/extensible';
import { PageModule } from '@abp/ng.components/page';
import { NgbDropdownModule, NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { PermissionManagementModule } from '@abp/ng.permission-management';
import { NgxValidateCoreModule } from '@ngx-validate/core';

@NgModule({
  imports: [
    CoreModule,
    ThemeSharedModule,
    UserManagmentRoutingModule,
    NgbNavModule,
    ExtensibleModule,
    NgbDropdownModule,
    PermissionManagementModule,
    NgxValidateCoreModule,
    PageModule,
    IdentityModule
  ],
})
export class UserManagmentModule {
  static forChild(): ModuleWithProviders<UserManagmentModule> {
    return {
      ngModule: UserManagmentModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<UserManagmentModule> {
    return new LazyModuleFactory(UserManagmentModule.forChild());
  }
}
