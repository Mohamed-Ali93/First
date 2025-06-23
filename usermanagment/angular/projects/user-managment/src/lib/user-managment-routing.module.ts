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
          import('./components/user-managment.component').then(m => m.UserManagmentComponent),
      },
      {
        path: 'security-logs',
        loadComponent: () => import('./components/security-logs.component').then(m => m.SecurityLogsComponent),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserManagmentRoutingModule {}
