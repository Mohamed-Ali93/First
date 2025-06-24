import { Component, OnInit, OnDestroy, ElementRef, Renderer2 } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { IdentityRoleService, IdentityRoleDto, IdentityRoleCreateDto, IdentityRoleUpdateDto, GetIdentityRolesInput } from '@abp/ng.identity/proxy';
import { UserManagmentService } from '../../services/user-managment.service';

@Component({
  selector: 'app-roles-with-user-count',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './roles-with-user-count.component.html',
  styleUrls: ['roles-with-user-count.component.scss'],
})
export class RolesWithUserCountComponent implements OnInit, OnDestroy {
  roles: (IdentityRoleDto & { userCount?: number; dropdownOpen: boolean })[] = [];
  totalCount = 0;
  loading = false;
  error: string | null = null;
  page = 1;
  pageSize = 10;

  roleForm: FormGroup;
  editingRole: IdentityRoleDto | null = null;
  isModalVisible = false;

  private globalClickUnlistener: (() => void) | undefined;

  constructor(
    private userManagmentService: UserManagmentService,
    private roleService: IdentityRoleService,
    private fb: FormBuilder,
    private el: ElementRef,
    private renderer: Renderer2
  ) {
    this.roleForm = this.fb.group({
      name: ['', Validators.required],
      isDefault: [false],
      isPublic: [false],
    });
  }

  ngOnInit(): void {
    this.loadRoles();
    this.globalClickUnlistener = this.renderer.listen('document', 'click', (event: Event) => {
      if (!this.el.nativeElement.contains(event.target)) {
        this.roles.forEach(r => r.dropdownOpen = false);
      }
    });
  }

  ngOnDestroy(): void {
    if (this.globalClickUnlistener) {
      this.globalClickUnlistener();
    }
  }

  loadRoles() {
    this.loading = true;
    this.roleService.getList({
      skipCount: (this.page - 1) * this.pageSize,
      maxResultCount: this.pageSize
    } as GetIdentityRolesInput).subscribe({
      next: (res) => {
        this.userManagmentService.getRolesWithUserCount().subscribe({
          next: (userCounts) => {
            this.roles = res.items.map(role => ({
              ...role,
              userCount: userCounts.find((u: any) => u.id === role.id)?.userCount ?? 0,
              dropdownOpen: false
            }));
            this.totalCount = res.totalCount;
            this.loading = false;
          },
          error: () => {
            this.roles = res.items.map(role => ({ ...role, dropdownOpen: false }));
            this.totalCount = res.totalCount;
            this.loading = false;
          }
        });
      },
      error: () => {
        this.error = 'Failed to load roles.';
        this.loading = false;
      }
    });
  }

  addRole() {
    this.editingRole = null;
    this.roleForm.reset();
    this.isModalVisible = true;
  }

  editRole(role: IdentityRoleDto) {
    this.editingRole = role;
    this.roleForm.patchValue({
      name: role.name,
      isDefault: role.isDefault,
      isPublic: role.isPublic
    });
    this.isModalVisible = true;
  }

  saveRole() {
    if (this.roleForm.invalid) return;
    const formValue = this.roleForm.value;
    this.loading = true;
    if (this.editingRole) {
      const updateDto: IdentityRoleUpdateDto = {
        name: formValue.name,
        isDefault: formValue.isDefault,
        isPublic: formValue.isPublic,
        concurrencyStamp: this.editingRole.concurrencyStamp
      };
      this.roleService.update(this.editingRole.id!, updateDto).subscribe({
        next: () => {
          this.isModalVisible = false;
          this.loadRoles();
        },
        error: () => {
          this.error = 'Failed to update role.';
          this.loading = false;
        }
      });
    } else {
      const createDto: IdentityRoleCreateDto = {
        name: formValue.name,
        isDefault: formValue.isDefault,
        isPublic: formValue.isPublic
      };
      this.roleService.create(createDto).subscribe({
        next: () => {
          this.isModalVisible = false;
          this.loadRoles();
        },
        error: () => {
          this.error = 'Failed to create role.';
          this.loading = false;
        }
      });
    }
  }

  deleteRole(role: IdentityRoleDto) {
    if (!confirm('Are you sure you want to delete this role?')) return;
    this.loading = true;
    this.roleService.delete(role.id!).subscribe({
      next: () => this.loadRoles(),
      error: () => {
        this.error = 'Failed to delete role.';
        this.loading = false;
      }
    });
  }

  pageChanged(newPage: number) {
    this.page = newPage;
    this.loadRoles();
  }

  toggleDropdown(role: any) {
    if (role.dropdownOpen) {
      role.dropdownOpen = false;
    } else {
      this.roles.forEach(r => r.dropdownOpen = false);
      role.dropdownOpen = true;
    }
  }
} 