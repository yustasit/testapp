import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';

import { UserApiService } from 'core/api/user-api.service';
import { UserModel } from 'core/models';
import { AppDialogComponent } from './app-dialog/app-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
})
export class AppComponent implements AfterViewInit {
    displayedColumns: string[] = ['id', 'username', 'password', 'fullName', 'eMail', 'phone', 'deleteBtn'];
    data: UserModel[];

    resultsLength = 0;
    isLoadingResults = true;
    isRateLimitReached = false;
    dialogRef: any;

    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: false }) sort: MatSort;

    constructor(private _userApiService: UserApiService,
        private snackBar: MatSnackBar,
        public _matDialog: MatDialog) { }

    ngAfterViewInit() {

        this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

        merge(this.sort.sortChange, this.paginator.page)
            .pipe(
                startWith({}),
                switchMap(() => {
                    this.isLoadingResults = true;
                    return this.getUsers()
                }),
                map(data => {
                    this.isLoadingResults = false;
                    this.isRateLimitReached = false;
                    this.resultsLength = data.count;

                    return data.response;
                }),
                catchError(() => {
                    this.isLoadingResults = false;
                    this.isRateLimitReached = true;
                    return observableOf([]);
                })
            ).subscribe(data => this.data = data);
    }

	appDialog(user?: UserModel): void {
		if (user === undefined) {
			user = null;
		}
        this.dialogRef = this._matDialog.open(AppDialogComponent, {
            data: user
        });
        this.dialogRef.afterClosed()
            .subscribe(response => {
                if (response) this.getUsers().subscribe(data => this.data = data.response);
            });
    }

    getUsers() {
        const sortParam = `${this.sort.active},${this.sort.direction}`;
        return this._userApiService!.getUsers(sortParam, this.paginator.pageIndex, this.paginator.pageSize);
    }

    deleteUser(event, id: number) {
        event.stopPropagation();
        if (id) {
            this._userApiService.deleteUser(id).subscribe(response => {
                if (response) {
                    this.showSnackBar('Deleted ');
                    this.getUsers().subscribe(data => this.data = data.response);
                } else this.showSnackBar('Error');
            }, error => console.log(error));
        }
    }

    private showSnackBar(message: string) {
        this.snackBar.open(message, 'OK', {
            duration: 10000,
        });
    }
}






