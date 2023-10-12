import { createServer, Model } from "miragejs"
import { ItemFactory } from "./factories/Item";

export default function createMirageServer() {

  createServer(
    {
      models: {
        item: Model
      },

      factories: {
        item: ItemFactory
      },

      seeds(server){
        server.createList("item", 6, {
          listId : "list37"
        })
      },

      routes(){
        this.urlPrefix = "http://127.0.0.1:8081";

        this.get("/api/items/:listId", (schema, request) => {
          const items = schema.items.all()
          const result = items === undefined
            ? []
            : items.models.map(obj => obj.item);

          return result
        })
      }
    }
  )
}