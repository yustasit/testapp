import { Component, Inject, ViewEncapsulation, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserApiService } from 'core/api/user-api.service';
import { UserModel } from '../../core/models';
import { MatSnackBar } from '@angular/material';

@Component({
    selector: 'app-dialog',
    templateUrl: './app-dialog.component.html',
    styleUrls: ['./app-dalog.component.css'],
    encapsulation: ViewEncapsulation.None
})
export class AppDialogComponent implements OnInit {

    userForm: FormGroup;

    constructor(
        public matDialogRef: MatDialogRef<AppDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private _user: UserModel,
        private _formBuilder: FormBuilder,
        private _userApiService: UserApiService,
        private snackBar: MatSnackBar,
    ) {
        this.userForm = this.initUserForm();
    }

    ngOnInit() {
        if (this._user !== null) {
            this.userForm.patchValue(this._user);
        }
    }

    initUserForm(): FormGroup {
        return this._formBuilder.group({
            id: [0],
            username: ['', Validators.required],
            password: ['', Validators.required],
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            eMail: ['', Validators.required],
            phone: ['', Validators.required]
        });
    }

    saveUser() {
        if (this.userForm.valid) {

            const user: UserModel = Object.assign({}, this.userForm.value);

            const request = this._user ?
                this._userApiService.updateUser(user) :
                this._userApiService.addUser(user);

            request.subscribe(response => {
                if (response) {
                    this.showSnackBar('Saved');
                    this.matDialogRef.close(true);
                } else this.matDialogRef.close(false)
            });
        }
    }

    private showSnackBar(message: string) {
        this.snackBar.open(message, 'OK', {
            duration: 10000,
        });
    }
}
