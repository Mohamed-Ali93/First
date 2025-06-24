import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { UserManagmentRoutingModule } from './user-managment-routing.module';

@NgModule({
  imports: [
    CoreModule,
    ThemeSharedModule,
    UserManagmentRoutingModule
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
