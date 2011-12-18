using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace SharpBullet.Parsers
{
    public partial class ParserSandbox : Form
    {
        public ParserSandbox()
        {
            InitializeComponent();
        }

        HtmlDocument document;

        private void btnParse_Click(object sender, EventArgs e)
        {
            document = new HtmlDocument();
            document.Parse(textBox1.Text);

            textBox2.Text = document.Root.GetAllText(false, true);

            print_r(document.Root, null);
        }

        void print_r(HtmlElement element, TreeNode node)
        {
            if (node == null) treeView1.Nodes.Clear();

            if (node == null)
                node = treeView1.Nodes.Add(element.GetTagName(), element.GetTagName());
            else
                node = node.Nodes.Add(element.GetTagName(), element.GetTagName() + " (" + (element.InnerText??element.Tag) + ")");

            if (element.ChildCount <= 0) return;

            foreach (HtmlElement child in element.Childs)
            {
                print_r(child, node);
            }

            treeView1.ExpandAll();
        }

        private void btnCompileRun_Click(object sender, EventArgs e)
        {
            string code = null;
            code += "using System;\r\n";
            code += "using TextIndex;\r\n";
            code += "using System.Collections;\r\n";
            
            code += "using System.Data;\r\n";
            code += "public class TestParser : SiteParser{ ";
            code += "    public override string Parse(HtmlDocument document) { ";
            code += "       " + textCode.Text;
            code += "    } ";
            code += "}";

            // CodeDOM'dan C# compiler'ı elde edelim
            Microsoft.CSharp.CSharpCodeProvider cp = new Microsoft.CSharp.CSharpCodeProvider();
            System.CodeDom.Compiler.ICodeCompiler ic = cp.CreateCompiler();

            // compiler parametrelerini ayarlayalım
            System.CodeDom.Compiler.CompilerParameters cpar = new System.CodeDom.Compiler.CompilerParameters();
            cpar.GenerateInMemory = true;
            cpar.GenerateExecutable = false;
            cpar.ReferencedAssemblies.Add("system.dll");
            cpar.ReferencedAssemblies.Add("System.Drawing.dll");
            cpar.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            cpar.ReferencedAssemblies.Add("System.Data.dll");
            cpar.ReferencedAssemblies.Add(Application.ExecutablePath);

            // kodu derletim ve sonuçları alalım
            System.CodeDom.Compiler.CompilerResults cr = ic.CompileAssemblyFromSource(cpar, code);

            // eğer derleme hatası varsa bu hataları bir bir gösterelim.
            foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
            {
                // hata mesajı, hatanın oluştuğu satır, önceki üç satır ve sonraki üç satırı içeren
                // bir hata mesajı hazırlayalım.
                string[] srcArr = code.Split('\n');
                string errMessage = ce.ErrorText + " at line " + (ce.Line - 1) + "\n\n";
                for (int i = ce.Line - 3; i < ce.Line + 3; i++)
                {
                    if (i < 0 || i >= srcArr.Length)
                        continue;
                    errMessage += i + " " + srcArr[i] + "\n";
                }
                // hatayı gösterelim
                MessageBox.Show(errMessage);

                return;
            }

            // kod hatasız derlendiyse test işlemlerine başlayalım.
            if (cr.Errors.Count == 0 && cr.CompiledAssembly != null)
            {
                // test edilecek iki koda (sınıfa) ait tipleri elde edelim.
                Type type = cr.CompiledAssembly.GetType("TestParser");
                try
                {
                    SiteParser parser = (SiteParser)Activator.CreateInstance(type);
                    string result = parser.Parse(document);
                    textBox2.Text = result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
