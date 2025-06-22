import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44301/',
  redirectUri: baseUrl,
  clientId: 'First_App',
  responseType: 'code',
  scope: 'offline_access First',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'First',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44301',
      rootNamespace: 'First',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
