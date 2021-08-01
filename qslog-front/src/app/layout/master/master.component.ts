import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
declare function init(): any;

@Component({
  selector: 'app-master',
  templateUrl: './master.component.html',
  styleUrls: ['./master.component.css']
})
export class MasterComponent implements OnInit {

  constructor(
    private router: Router
  ) { 
    this.router.events.subscribe((e) => {
      if (e instanceof NavigationEnd) {
        init();
      }
   });
  }

  ngOnInit(): void {
  }

}
