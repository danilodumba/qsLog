import { FormGroup } from "@angular/forms";
import { BsModalRef } from "ngx-bootstrap/modal";
import { AlertService } from "./alert.service";
import { FormComponent } from "./form.component";

export abstract class FormModalComponent extends FormComponent {

    principalForm!: FormGroup;
    id: any; 
    title: string = '';

    constructor(
        public bsModalRef: BsModalRef,
        public alertService: AlertService
    ) {
        super(alertService);
    }

    close() {
        this.bsModalRef.hide();
    }
}