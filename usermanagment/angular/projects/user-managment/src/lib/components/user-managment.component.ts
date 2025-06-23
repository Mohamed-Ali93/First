import { Component, inject } from '@angular/core';
import { UserManagmentService } from '../services/user-managment.service';

@Component({
  standalone: true,
  selector: 'lib-user-managment',
  template: `
    <h1>User Management Root Works!</h1>
    <p>This is the default page for the User Management module.</p>
  `,
})
export class UserManagmentComponent {
  protected readonly service = inject(UserManagmentService);

  constructor() {
    this.service.sample().subscribe(console.log);
  }
}
