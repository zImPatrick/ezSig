using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
class Application
{
    [STAThread]
    static void Main(string[] args)
    {
        string clipboard = Clipboard.GetText();

        if (args[0] == "--sha256")
        {
            using (var sha = SHA256.Create())
            {
                using (var stream = File.OpenRead(args[1]))
                {
                    var hash = sha.ComputeHash(stream);
                    compare(BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant(), clipboard);
                }
            }
        }
        else if (args[0] == "--md5")
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(args[1]))
                {
                    var hash = md5.ComputeHash(stream);
                    compare(BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant(), clipboard);
                }
            }
        }
    }

    static void compare(string first, string second)
    {
        if(first == second)
        {
            MessageBox.Show("The file signature matches your clipboard.", "ezSig", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } else
        {
            MessageBox.Show("The Signature \n" + first + "\n doesn't match your clipboard \n(" + second + ")", "Signature doesn't match", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
