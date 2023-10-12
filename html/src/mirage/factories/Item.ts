import { Factory } from "miragejs"
import { faker } from "@faker-js/faker"

const listId = "list" + faker.datatype.number();
const listName = faker.lorem.word();
  
export const ItemFactory = Factory.extend({
    item(){
      const id = faker.datatype.uuid();
      return { 
        Id: id,
        ListId: faker.datatype.number(),
        Title: faker.lorem.word(),
        ListName: listName,
        Description: faker.lorem.word()
      }
    },
    listId: listId,
  })
