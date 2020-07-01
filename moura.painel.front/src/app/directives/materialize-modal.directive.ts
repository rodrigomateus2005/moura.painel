import { Directive, OnInit, ElementRef } from '@angular/core';

import * as M from 'materialize-css';
import { Observable, Subscriber } from 'rxjs';

@Directive({
  selector: '[appMaterializeModal]',
  exportAs: 'materializeModal'
})
export class MaterializeModalDirective implements OnInit {

  private instance: any;

  private observableClose: Observable<void>;
  private observerClose: Subscriber<void>;

  constructor(private element: ElementRef) {
    this.instance = M.Modal.init(this.element.nativeElement, {
      onCloseEnd: () => {
        this.onCloseEnd();
      }
    });

    this.observableClose = new Observable<void>(observer => {
      this.observerClose = observer;
    });
  }

  ngOnInit(): void {

  }

  public abrir(): Promise<void> {
    return new Promise(resolve => {
      this.instance.open();
      const subscription = this.observableClose.subscribe(() => {
        resolve();
        subscription.unsubscribe();
      });
    });
  }

  public fechar() {
    this.instance.close();
  }

  private onCloseEnd() {
    this.observerClose.next();
  }

}
