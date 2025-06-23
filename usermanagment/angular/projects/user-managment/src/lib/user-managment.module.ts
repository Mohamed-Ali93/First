import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { UserManagmentComponent } from './components/user-managment.component';
import { UserManagmentRoutingModule } from './user-managment-routing.module';
import { SecurityLogsComponent } from './components/security-logs.component';

@NgModule({
  imports: [CoreModule, ThemeSharedModule, UserManagmentRoutingModule, UserManagmentComponent, SecurityLogsComponent],
  exports: [UserManagmentComponent, SecurityLogsComponent],
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
