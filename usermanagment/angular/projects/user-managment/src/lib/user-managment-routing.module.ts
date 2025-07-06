import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RouterOutletComponent } from '@abp/ng.core';

const routes: Routes = [
  {
    path: '',
    component: RouterOutletComponent,
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./components/user-managment/user-managment.component').then(m => m.UserManagmentComponent),
      },
      {
        path: 'security-logs',
        loadComponent: () => import('./components/security-logs/security-logs.component').then(m => m.SecurityLogsComponent),
      },
      {
        path: 'roles-with-user-count',
        loadComponent: () => import('./components/roles-with-user-count/roles-with-user-count.component').then(m => m.RolesWithUserCountComponent),
      },
      {
        path: 'custom-role',
        loadComponent: () => import('./components/identity/roles/custom-role/custom-role.component').then(m => m.CustomRoleComponent),
      },
      {
        path: 'user-role-management',
        loadComponent: () => import('./components/user-role-management/user-role-management.component').then(m => m.UserRoleManagementComponent),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserManagmentRoutingModule {}
