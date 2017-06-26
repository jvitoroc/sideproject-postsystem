const express = require("express");
const mongoose = require("mongoose");
const post = require("./models/post");
app = express();

mongoose.connect('mongodb://127.0.0.1:27017/blog');

app.set("view engine", "ejs");

app.use(express.static("public"));

mongoose.connection.once('open',()=>{
    console.log('Connection has been made');
}).on('error',(err)=>{

});

app.get('/posts',(req,res)=>{
    post.find({}).then((data)=>{
        res.render('posts', {posts:data});
        console.log(data);
    });
});

app.listen(3000);
