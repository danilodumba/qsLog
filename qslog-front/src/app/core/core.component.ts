import { BlockUI, NgBlockUI } from "ng-block-ui";
import { AlertService } from "./alert.service";

export abstract class CoreComponent {
    @BlockUI() blockUI!: NgBlockUI;

    constructor(protected alertService: AlertService) {

    }

    protected startLoading() {
        this.blockUI.start('Carregando...'); // Start blocking
    }

    protected endLoading() {
        this.blockUI.stop();
    }

    protected catchError(e: any) {
        if (e.status == 400) {
            this.catchError400(e);
        }
        console.log(e);
        this.endLoading();
    }

    private catchError400(e: any) {
        if (e.error.length > 0) {
            let error = '<ul>';
            for (var i = 0; i < e.error.length; i++) {
                error += '<li>' + e.error[i].description + '</li>'
            }

            error += '</ul>';

            this.alertService.showWarning('Atenção', error);
        }
    }
}