import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RouterOutletComponent } from '@abp/ng.core';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: RouterOutletComponent,
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./components/user-managment.component').then(m => m.UserManagmentComponent),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserManagmentRoutingModule {}
