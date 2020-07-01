import { Component, OnInit, ViewChild } from '@angular/core';
import { MaterializeModalDirective } from 'src/app/directives/materialize-modal.directive';
import { Observable, Subscriber } from 'rxjs';

@Component({
  selector: 'app-carregando',
  templateUrl: './carregando.component.html',
  styleUrls: ['./carregando.component.scss']
})
export class CarregandoComponent implements OnInit {

  public mensagem = 'Carregando <span class="marquee"> ...</span>';

  private observableCreate: Observable<void>;
  private observerCreate: Subscriber<void>;

  private $modal: MaterializeModalDirective;
  public get modal(): MaterializeModalDirective {
    return this.$modal;
  }
  @ViewChild('modal')
  public set modal(value: MaterializeModalDirective) {
    this.$modal = value;
    this.observerCreate.next();
  }

  constructor() {
    this.observableCreate = new Observable<void>(observer => {
      this.observerCreate = observer;
    });
  }

  ngOnInit(): void {
  }

  public abrir(): Promise<void> {
    return new Promise<void>(resolve => {
      if (this.modal) {
        this.modal.abrir().then(resolve);
      } else {
        const subscription = this.observableCreate.subscribe(() => {
          this.modal.abrir().then(resolve);
          subscription.unsubscribe();
        });
      }
    });
  }

  public fechar() {
    this.modal.fechar();
  }

}
