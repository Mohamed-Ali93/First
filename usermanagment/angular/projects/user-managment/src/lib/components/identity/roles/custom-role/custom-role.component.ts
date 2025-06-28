import { ListService, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { IdentityRoleDto, IdentityRoleService } from '@abp/ng.identity/proxy';
import { ePermissionManagementComponents } from '@abp/ng.permission-management';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import {
  EXTENSIONS_IDENTIFIER,
  FormPropData,
  generateFormFromProps,
} from '@abp/ng.components/extensible';
import { Component, inject, Injector, OnInit, AfterViewInit } from '@angular/core';
import { UntypedFormGroup, FormBuilder, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { eIdentityComponents } from '@volo/abp.ng.identity';
import { CommonModule } from '@angular/common';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { ExtensibleModule } from '@abp/ng.components/extensible';
import { PermissionManagementModule } from '@abp/ng.permission-management';
import { PageModule } from '@abp/ng.components/page';
import { LocalizationModule } from '@abp/ng.core';
import { IdentityModule } from '@volo/abp.ng.identity';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { UserManagmentService } from '../../../../services/user-managment.service';

@Component({
  selector: 'lib-custom-role',
  standalone: true,
  imports: [
    CommonModule,
    ThemeSharedModule,
    ExtensibleModule,
    PermissionManagementModule,
    PageModule,
    LocalizationModule,
    IdentityModule,
    NgbDropdownModule,
    ReactiveFormsModule
  ],
  templateUrl: './custom-role.component.html',
  styleUrls: ['./custom-role.component.scss'],
  providers: [
    ListService,
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: eIdentityComponents.Roles,
    }
  ],
})
export class CustomRoleComponent implements OnInit, AfterViewInit {
  protected readonly list = inject(ListService<PagedAndSortedResultRequestDto>);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly toasterService = inject(ToasterService);
  private readonly injector = inject(Injector);
  protected readonly service = inject(IdentityRoleService);
  private readonly fb = inject(FormBuilder);
  private readonly userManagmentService = inject(UserManagmentService);

  data: PagedResultDto<IdentityRoleDto & { userCount?: number }> = { items: [], totalCount: 0 };

  form!: UntypedFormGroup;

  selected?: IdentityRoleDto;

  isModalVisible!: boolean;

  visiblePermissions = false;

  providerKey?: string;

  modalBusy = false;

  loadingUserCounts = false;

  permissionManagementKey = ePermissionManagementComponents.PermissionManagement;

  onVisiblePermissionChange = (event: boolean) => {
    this.visiblePermissions = event;
  };

  ngOnInit() {
    this.hookToQuery();
  }

  ngAfterViewInit() {
    // Manually initialize Bootstrap dropdowns
    // @ts-ignore
    const bootstrap = window['bootstrap'];
    if (bootstrap && bootstrap.Dropdown) {
      document.querySelectorAll('.dropdown-toggle').forEach((dropdownToggleEl: any) => {
        new bootstrap.Dropdown(dropdownToggleEl);
      });
    }
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selected?.name || '', [Validators.required, Validators.maxLength(64)]],
      isDefault: [this.selected?.isDefault || false],
      isPublic: [this.selected?.isPublic || false],
    });
  }

  openModal() {
    this.buildForm();
    this.isModalVisible = true;
  }

  add() {
    this.selected = {} as IdentityRoleDto;
    this.openModal();
  }

  edit(id: string) {
    this.service.get(id).subscribe(res => {
      this.selected = res;
      this.openModal();
    });
  }

  save() {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      return;
    }
    this.modalBusy = true;
    const { id } = this.selected || {};
    const formValue = { ...this.selected, ...this.form.value };
    (id
      ? this.service.update(id, formValue)
      : this.service.create(this.form.value)
    )
      .pipe(finalize(() => (this.modalBusy = false)))
      .subscribe(() => {
        this.isModalVisible = false;
        this.toasterService.success('AbpUi::SavedSuccessfully');
        this.list.get();
      });
  }

  delete(id: string, name: string) {
    this.confirmationService
      .warn('AbpIdentity::RoleDeletionConfirmationMessage', 'AbpIdentity::AreYouSure', {
        messageLocalizationParams: [name],
      })
      .subscribe((status: Confirmation.Status) => {
        if (status === Confirmation.Status.confirm) {
          this.toasterService.success('AbpUi::DeletedSuccessfully');
          this.service.delete(id).subscribe(() => this.list.get());
        }
      });
  }

  private hookToQuery() {
    this.list.hookToQuery(query => this.service.getList(query)).subscribe(res => {
      // Get user counts for each role
      this.loadingUserCounts = true;
      this.userManagmentService.getRolesWithUserCount().subscribe({
        next: (userCounts) => {
          this.data = {
            ...res,
            items: res.items.map(role => ({
              ...role,
              userCount: userCounts.find((u: any) => u.id === role.id)?.userCount ?? 0
            }))
          };
          this.loadingUserCounts = false;
        },
        error: () => {
          // If user count fetch fails, still show roles without counts
          this.data = {
            ...res,
            items: res.items.map(role => ({ ...role, userCount: 0 }))
          };
          this.loadingUserCounts = false;
        }
      });
    });
  }

  openPermissionsModal(providerKey: string) {
    this.providerKey = providerKey;
    setTimeout(() => {
      this.visiblePermissions = true;
    }, 0);
  }

  sort(data: any) {
    const { prop, dir } = data.sorts[0];
    this.list.sortKey = prop;
    this.list.sortOrder = dir;
  }
}

