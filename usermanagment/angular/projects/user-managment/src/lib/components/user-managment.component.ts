import { Component, inject } from '@angular/core';
import { UserManagmentService } from '../services/user-managment.service';

@Component({
  standalone: true,
  selector: 'lib-user-managment',
  template: ` <p>user-managment works!</p> `,
})
export class UserManagmentComponent {
  protected readonly service = inject(UserManagmentService);

  constructor() {
    this.service.sample().subscribe(console.log);
  }
}
