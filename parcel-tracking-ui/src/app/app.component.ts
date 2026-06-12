import { Component } from '@angular/core';
import { ParcelsComponent } from './components/parcels/parcels.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ParcelsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'parcel-tracking-ui';
}