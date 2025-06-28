import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { IdentityRoleService, IdentityRoleDto } from '@abp/ng.identity/proxy';
import { IdentityUserService, IdentityUserDto } from '@abp/ng.identity/proxy';
import { UserManagmentService, MoveUserToRoleDto } from '../../services/user-managment.service';
import { ToasterService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-user-role-management',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './user-role-management.component.html',
  styleUrls: ['./user-role-management.component.scss'],
})
export class UserRoleManagementComponent implements OnInit {
  roles: IdentityRoleDto[] = [];
  users: IdentityUserDto[] = [];
  selectedRole: IdentityRoleDto | null = null;
  selectedUser: IdentityUserDto | null = null;
  loading = false;
  error: string | null = null;

  moveUserForm: FormGroup;
  isMoveModalVisible = false;

  constructor(
    private userManagmentService: UserManagmentService,
    private roleService: IdentityRoleService,
    private userService: IdentityUserService,
    private toasterService: ToasterService,
    private fb: FormBuilder
  ) {
    this.moveUserForm = this.fb.group({
      userId: ['', Validators.required],
      sourceRoleId: ['', Validators.required],
      targetRoleId: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.loadRoles();
  }

  loadRoles() {
    this.loading = true;
    this.roleService.getList({ maxResultCount: 1000 }).subscribe({
      next: (res) => {
        this.roles = res.items;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load roles.';
        this.loading = false;
      }
    });
  }

  loadUsersInRole(roleId: string) {
    this.loading = true;
    this.userManagmentService.getUsersInRole(roleId).subscribe({
      next: (users) => {
        this.users = users;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load users for this role.';
        this.loading = false;
      }
    });
  }

  onRoleSelect(role: IdentityRoleDto) {
    this.selectedRole = role;
    this.loadUsersInRole(role.id!);
  }

  openMoveUserModal(user: IdentityUserDto, sourceRole: IdentityRoleDto) {
    this.selectedUser = user;
    this.moveUserForm.patchValue({
      userId: user.id,
      sourceRoleId: sourceRole.id,
      targetRoleId: ''
    });
    this.isMoveModalVisible = true;
  }

  moveUserToRole() {
    if (this.moveUserForm.invalid) return;

    const formValue = this.moveUserForm.value;
    const moveDto: MoveUserToRoleDto = {
      userId: formValue.userId,
      sourceRoleId: formValue.sourceRoleId,
      targetRoleId: formValue.targetRoleId,
      userName: this.selectedUser?.userName,
      sourceRoleName: this.roles.find(r => r.id === formValue.sourceRoleId)?.name,
      targetRoleName: this.roles.find(r => r.id === formValue.targetRoleId)?.name
    };

    this.loading = true;
    this.userManagmentService.moveUserToRole(moveDto).subscribe({
      next: () => {
        this.toasterService.success('User moved to role successfully!');
        this.isMoveModalVisible = false;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to move user to role.';
        this.loading = false;
      }
    });
  }

  closeMoveModal() {
    this.isMoveModalVisible = false;
    this.moveUserForm.reset();
  }
} 