using System;
using System.Windows.Forms;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;
using Microsoft.VisualBasic;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        public IMongoClient _client;
        public IMongoDatabase _database;
        public string collectionName = "posts";
        public Form1()
        {
            InitializeComponent();

        }
        private void setContentEnablish(bool crit)
        {
            body.Enabled = crit;
            title.Enabled = crit;
            databaseName.Enabled = crit;
            checkBox1.Enabled = crit;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            setContentEnablish(false);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool titleNotOk = title.Text == "" || title.TextLength == 0;
            bool bodyNotOk = body.Text == "" || body.TextLength == 0;
            bool dbNotOk = databaseName.Text == "" || databaseName.TextLength == 0;
            if (titleNotOk)
            {
                MessageBox.Show("Title is not long enough", "Are you missing something?", MessageBoxButtons.OK);
            }
            else if (bodyNotOk)
            {
                MessageBox.Show("Body is not long enough", "Are you missing something?", MessageBoxButtons.OK);
            }
            else if (dbNotOk)
            {
                MessageBox.Show("You need to provide a database", "Are you missing something?", MessageBoxButtons.OK);
            }
            else
            {

                string UserAnswer;
                UserAnswer = Interaction.InputBox("Specify the collection", "MongoDB Post", collectionName);
                if (UserAnswer != "")
                    collectionName = UserAnswer;
                    _database = _client.GetDatabase(databaseName.Text);
                    MongoDBPost post = new MongoDBPost(title.Text, body.Text);
                    post.send(_database, UserAnswer);
                
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string UserAnswer;
            UserAnswer = Interaction.InputBox("Specify the MongoDB to connect", "MongoDB Post", "mongodb://localhost:27017");
            if (UserAnswer != "")
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(UserAnswer));
                _client = new MongoClient(settings);
                setContentEnablish(true);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.CheckState == CheckState.Checked)
            {
                databaseName.ReadOnly = true;
            }
            else
            {
                databaseName.ReadOnly = false;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Close();
        }

    }

    public class MongoDBPost
    {

        private string mTitle;
        private string mBody;

        public MongoDBPost(string title, string body)
        {
            mTitle = title;
            mBody = body;
        }

        public MongoDBPost()
        {
        }

        public async void send(IMongoDatabase database, string collectionName)
        {

            string currentDate = DateTime.Now.ToString("d/M/yyyy");
            string currentHour = DateTime.Now.ToString("HH:mm:ss");

            var document = new BsonDocument
            {
                {"title",mTitle },
                {"body",mBody },
                {"date",currentDate},
                {"hour",currentHour}
            };

            var collection = database.GetCollection<BsonDocument>(collectionName);
            await collection.InsertOneAsync(document);
        }

        public string Title
        {
            get { return mTitle; }
            set { mTitle = value; }
        }

        public string Body
        {
            get { return mBody; }
            set { mTitle = value; }
        }

    }

}
