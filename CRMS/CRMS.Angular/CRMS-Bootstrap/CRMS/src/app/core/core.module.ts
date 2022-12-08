import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HttpClient, HttpXhrBackend, HttpBackend, HttpHeaders } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthenticationService } from './authentication/authentication.service';
import { AuthenticationGuardService } from './authentication/authentication-guard.service';
import { HttpCacheService } from './http/http-cache.service';
import { AuthcredentialstoreService } from './authentication/authcredentialstore.service';

/* export function createHttpService(backend: HttpBackend,
  defaultOptions: HttpHeaders,
  httpCacheService: HttpCacheService,
  authStore: AuthcredentialstoreService) {
  return new HttpService(backend, defaultOptions, httpCacheService, authStore);
} */

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    NgbModule,
    RouterModule
  ],
  declarations: [
    
  ],
  providers: [
    AuthenticationService,
    AuthenticationGuardService,
    AuthcredentialstoreService,
    HttpCacheService
    /* {
      provide: HttpClient,
      deps: [HttpXhrBackend, HttpHeaders, HttpCacheService, AuthcredentialstoreService],
      useFactory: createHttpService
    } */
  ]
})
export class CoreModule {

  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    // Import guard
    if (parentModule) {
      throw new Error(`${parentModule} has already been loaded. Import Core module in the AppModule only.`);
    }
  }

}
