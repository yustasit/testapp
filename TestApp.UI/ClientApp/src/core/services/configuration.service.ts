import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Title } from '@angular/platform-browser';

import { environment } from "../../environments/environment";

@Injectable()
export class ConfigurationService {

    constructor(
        private http: HttpClient,
        private title: Title
    ) { }

	public loadConfig(): Promise<boolean> {
		return new Promise((resolve, reject) => {
			resolve(true);
		});
	}

    public getApiUrl(): string {
        return environment.apiUrl;
    }

    public getCompanyName(): string {
        return environment.companyName;
    }
}
