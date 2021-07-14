import { BlockUI, NgBlockUI } from "ng-block-ui";

export abstract class CoreComponent {
    @BlockUI() blockUI!: NgBlockUI;

    protected startLoading() {
        this.blockUI.start('Carregando...'); // Start blocking
    }

    protected endLoading() {
        this.blockUI.stop();
    }

}