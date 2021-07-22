import { FormGroup } from "@angular/forms";
import { BsModalRef } from "ngx-bootstrap/modal";
import { AlertService } from "./alert.service";
import { CoreComponent } from "./core.component";

export abstract class FormModalComponent extends CoreComponent {

    principalForm!: FormGroup;
    id: any; 
    title: string = '';

    constructor(
        public bsModalRef: BsModalRef,
        public alertService: AlertService
    ) {
        super(alertService);
    }

    isInvalidField(fieldName: string): boolean {
        const field = this.principalForm.controls[fieldName];
        if (field.invalid && field.touched) {
            return true;
        }
        return false;
    }

    classInvalidField(fieldName: string): string {
        if (this.isInvalidField(fieldName)) {
            return 'is-invalid';
        }

        return '';
    }

    close() {
        this.bsModalRef.hide();
    }

    abstract createForm(): void;
}