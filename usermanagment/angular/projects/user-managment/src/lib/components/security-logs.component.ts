import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgClass, CommonModule } from '@angular/common';
import { SecurityLogsService } from '../services/security-logs.service';
import { SecurityLogDto, GetSecurityLogInput } from '../models/security-log.model';

@Component({
  selector: 'lib-security-logs',
  templateUrl: './security-logs.component.html',
  styleUrls: ['./security-logs.component.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule, NgClass],
})
export class SecurityLogsComponent implements OnInit {
  logs: SecurityLogDto[] = [];
  totalCount = 0;
  page = 0;
  filter: GetSecurityLogInput = { skipCount: 0, maxResultCount: 10, sorting: 'creationTime DESC' };
  loading = false;
  error: string | null = null;
  sortField: string = 'creationTime';
  sortDirection: 'ASC' | 'DESC' = 'DESC';
  selectedLog: SecurityLogDto | null = null;

  get totalPages() {
    return Math.ceil(this.totalCount / (this.filter.maxResultCount ?? 10));
  }

  constructor(private service: SecurityLogsService) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.loading = true;
    this.error = null;
    this.filter.sorting = `${this.sortField} ${this.sortDirection}`;
    this.service.getList(this.filter).subscribe({
      next: res => {
        this.logs = res.items;
        this.totalCount = res.totalCount;
        console.log('Loaded logs:', res.items);
        this.loading = false;
      },
      error: err => {
        this.error = 'Failed to load security logs.';
        this.loading = false;
      }
    });
  }

  search() {
    this.filter.skipCount = 0;
    this.page = 0;
    this.load();
  }

  sort(field: string) {
    if (this.sortField === field) {
      this.sortDirection = this.sortDirection === 'ASC' ? 'DESC' : 'ASC';
    } else {
      this.sortField = field;
      this.sortDirection = 'ASC';
    }
    this.load();
  }

  sortIcon(field: string) {
    if (this.sortField !== field) return 'fas fa-sort';
    return this.sortDirection === 'ASC' ? 'fas fa-sort-up' : 'fas fa-sort-down';
  }

  nextPage() {
    this.page++;
    this.filter.skipCount = this.page * (this.filter.maxResultCount ?? 10);
    this.load();
  }

  prevPage() {
    if (this.page > 0) {
      this.page--;
      this.filter.skipCount = this.page * (this.filter.maxResultCount ?? 10);
      this.load();
    }
  }

  showDetails(log: SecurityLogDto) {
    this.selectedLog = log;
  }

  closeDetails() {
    this.selectedLog = null;
  }
} 