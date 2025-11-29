import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./core/layout/navbar/navbar.component";
import { FooterComponent } from "./core/layout/footer/footer.component";
import { filter, map } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, FooterComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Survey Client';

  noLayout = false;

  constructor(private router: Router, private route: ActivatedRoute) {
    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd),
        map(() => {
          let child = this.route.firstChild;
          while (child?.firstChild) child = child.firstChild;
          return child?.snapshot.data['noLayout'];
        })
      )
      .subscribe((value) => this.noLayout = value || false);
  }
}
