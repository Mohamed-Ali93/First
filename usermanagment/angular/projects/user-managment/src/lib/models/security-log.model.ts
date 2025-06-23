export interface SecurityLogDto {
  id: string;
  tenantId?: string;
  userId?: string;
  userName: string;
  action: string;
  applicationName: string;
  identity: string;
  clientId: string;
  correlationId: string;
  clientIpAddress: string;
  browserInfo: string;
  extraProperties: string;
  creationTime: string;
}

export interface GetSecurityLogInput {
  userId?: string;
  userName?: string;
  action?: string;
  clientIpAddress?: string;
  startTime?: string;
  endTime?: string;
  skipCount?: number;
  maxResultCount?: number;
  sorting?: string;
} 