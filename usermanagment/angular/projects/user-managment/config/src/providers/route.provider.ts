import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eUserManagmentRouteNames } from '../enums/route-names';

export const USER_MANAGMENT_ROUTE_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureRoutes,
    deps: [RoutesService],
    multi: true,
  },
];

export function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/user-managment',
        name: eUserManagmentRouteNames.UserManagment,
        iconClass: 'fas fa-book',
        layout: eLayoutType.application,
        order: 3,
      },
      {
        path: '/user-managment/security-logs',
        name: 'Security Logs',
        parentName: eUserManagmentRouteNames.UserManagment,
        order: 1,
        iconClass: 'fas fa-shield-alt',
      },
    ]);
  };
}
