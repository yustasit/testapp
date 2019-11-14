import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';

export class BaseApiService {

	constructor(
		private readonly http: HttpClient
	) { }

	protected post<T>(url: string, body: any): Observable<T> {
		return this.http.post<T>(url, JSON.stringify(body));
	}
	protected get<T>(url: string): Observable<T> {
		return this.http.get<T>(url);
	}
	protected put<T>(url: string, body: any): Observable<T> {
		return this.http.put<T>(url, JSON.stringify(body));
	}

	protected delete<T>(url: string): Observable<T> {
		return this.http.delete<T>(url);
	}
}
