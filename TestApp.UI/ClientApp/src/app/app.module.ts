import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorModule } from '@angular/material/paginator';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { ConfigurationService } from 'core/services';
import { BaseHttpInterceptor } from 'core/api/interceptor';
import { AppDialogComponent } from './app-dialog/app-dialog.component';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule, MatButtonModule, MatSnackBarModule } from '@angular/material';
import { MatDialogModule } from '@angular/material/dialog';
import { UserApiService } from '../core/api/user-api.service';

export function loadConfig(configService: ConfigurationService) {
	return () => configService.loadConfig();
}


@NgModule({
	declarations: [
		AppComponent,
		AppDialogComponent
	],
	imports: [
		BrowserModule,
		BrowserAnimationsModule,
		HttpClientModule,
		MatSortModule,
		MatTableModule,
		MatProgressSpinnerModule,
		MatPaginatorModule,
		MatIconModule,
		MatFormFieldModule,
		FormsModule,
		ReactiveFormsModule,
		MatDialogModule,
		MatInputModule,
		MatButtonModule,
		MatSnackBarModule

	],
	entryComponents: [
		AppDialogComponent
	],
	providers: [
		UserApiService,
		ConfigurationService,
		{
			provide: APP_INITIALIZER,
			useFactory: loadConfig,
			deps: [ConfigurationService],
			multi: true
		},
		{
			provide: HTTP_INTERCEPTORS,
			useClass: BaseHttpInterceptor,
			deps: [ConfigurationService],
			multi: true
		}
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
