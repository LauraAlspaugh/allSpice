export class Ingredient {
    constructor(data) {
        this.id = data.id || data._id
        this.name = data.name
        this.quantity = data.quantity
        this.recipeId = data.recipeId
        this.creatorId = data.creatorId
        this.creator = data.creator
        this.edit = false

    }

}