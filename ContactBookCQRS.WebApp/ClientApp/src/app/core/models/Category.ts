import { Contact } from './Contact';

export class Category {
  id: string;
  contactBookId: string;
  name: string;
  contacts: Contact[];

  constructor(contactBookId: string,
    name:string) {
    this.contactBookId = contactBookId;
    this.name = name;
  }
}
