const mongoose = require("mongoose");

const schema = mongoose.Schema;

const postSchema = new schema({
    title: String,
    body: String,
    date: String,
    hour: String
});

const postModel = mongoose.model('posts', postSchema);

module.exports = postModel;
