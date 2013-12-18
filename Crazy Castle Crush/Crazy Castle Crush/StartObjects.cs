﻿using Microsoft.Xna.Framework;
using NOVA.Scenery;
using NOVA.Graphics;
using NOVA;
using Microsoft.Xna.Framework.Graphics;
using NOVA.UI;
using System.Collections.Generic;
using BEPUphysics.Constraints.SolverGroups;


namespace Crazy_Castle_Crush
{
    class StartObjects
    {
        BoxObject hintergrund;
        Vector3 pos = new Vector3(0, 0, -11);
        bool lastR = false;
        
        
        public StartObjects(Scene scene, Levels level)
        {
            this.scene = scene;
            this.level = level;
        }

        public BoxObject RightHand()
        {
            RenderMaterial gruen = new RenderMaterial();
            gruen.Diffuse = Color.Green.ToVector4();


            BoxObject rightHand = new BoxObject(new Vector3(0, 0, 0), new Vector3(0.5f, 0.5f, 0f), 0f);
            rightHand.RenderMaterial = gruen;
            rightHand.RenderMaterial.Transparency = 0.3f;
            
            scene.Add(rightHand);
            
            return rightHand;
        }

        public BoxObject LeftHand()
        {

            RenderMaterial rot = new RenderMaterial();
            rot.Diffuse = Color.Red.ToVector4();

            BoxObject leftHand = new BoxObject(new Vector3(0, 0, 0), new Vector3(0.5f, 0.5f, 0f), 0f);
            leftHand.RenderMaterial = rot;
            leftHand.RenderMaterial.Transparency = 0.3f;

            scene.Add(leftHand);

            return leftHand;
        }

        public BoxObject showObjects(string bild)
        {
            
            Vector3 vector = new Vector3(0, 2.0f, -6);

            RenderMaterial bildobjekte = new RenderMaterial();
            BoxObject blende = new BoxObject(vector, new Vector3(4, 0.8f, 0), 0f);
            bildobjekte.Texture = Core.Content.Load<Texture2D>(bild);
            bildobjekte.Diffuse = Color.White.ToVector4();
            blende.RenderMaterial = bildobjekte;

            scene.Add(blende);
            return blende;
            
        }

        public BoxObject LoadObjWafC()
        {
            BoxObject changer = LoadBox(new Vector3(0, 2.0f, -6), new Vector3(1, 0.8f, 0), 0f);
            RenderMaterial bild = new RenderMaterial();
            bild.Texture = Core.Content.Load<Texture2D>("pist");
            bild.Diffuse = Color.White.ToVector4();
            changer.RenderMaterial = bild;

            //scene.Add(changer);
            return changer;
        }

        public void einausblender(BoxObject Ob, BoxObject changer, int state, float time)
        {
            // 1=ObjSp1  11=WafSp1  12=TexSp1 2=ObjSp2  21=WafSp2  22=TexSp2  0=else
            
            if (state == 0)
            {
                rausblend(Ob, Ob.Position.X);
                rausblend(changer,changer.Position.X);
            }
            if (state == 1)
            {
                reinblend(Ob, level.getSpieler1Pos());
                reinblend(changer, level.getSpieler1Pos() + 3.5f);
                overblend(Ob, "Bau"); //Anzeige soll Bauobjekte Anzeigen
                overblend(changer,"pist"); //Rechts soll Waffe zum wechseln zum Waffenmenu angezeigt werden
            }
            if (state == 11)
            {
                overblend(Ob, "pist"); //Anzeige soll Waffenauswahl anzeigen
                overblend(changer,"Bau"); //Rechts soll Obj zum wechseln zum Objmenu angezeigt werden
            }
            if (state == 12)
            {
                rausblend(changer, changer.Position.X);
                overblend(Ob, "Material");
            }
            if (state == 2)
            {
                reinblend(changer, level.getSpieler2Pos() + 3.5f);
                reinblend(Ob, level.getSpieler2Pos());
                overblend(Ob, "Bau"); //Anzeige soll Bauobjekte Anzeigen
                overblend(changer,"pist"); //Rechts soll Waffe zum wechseln zum Waffenmenu angezeigt werden
            }
            if (state == 21)
            {
                overblend(Ob, "pist"); //Anzeige soll Waffenauswahl anzeigen
                overblend(changer,"Bau"); //Rechts soll Obj zum wechseln zum Objmenu angezeigt werden
            }
            if (state == 22)
            {
                rausblend(changer,changer.Position.X);
                overblend(Ob, "Material");
            }


        }

        private void rausblend(BoxObject box, float x)
        {
            //TODO: rausblenden
            if (lastR == false) { }//box.Visible = false; }
            box.Position = new Vector3(x, 4f, -6f);
            lastR = true;
        }
        private void reinblend(BoxObject box, float x)
        {
            //TODO: einblenden
            if (lastR) { }//box.Visible = true; }
            box.Physics.Position = new Vector3(x, 2f, -6f);
            lastR = false;
        }
        
        private void overblend(BoxObject box, string bild)
        {
            if (box.RenderMaterial.Texture.Name != bild)
            {
                RenderMaterial xmenu = new RenderMaterial();
                xmenu.Texture = Core.Content.Load<Texture2D>(bild);
                xmenu.Diffuse = Color.Gray.ToVector4();
                box.RenderMaterial = xmenu;
            }
        }
         



        public void LoadStartObjects(int level)
        {
            if (level == 1)
            {
                //Lädt Spieluntergrund
                /*ModelObject Kanonenrohr = new ModelObject(new Vector3(0, -1.5f, -5), Quaternion.Identity, new Vector3(1, 1, 1), CollisionType.ExactMesh, " ", "Kanonenrohr", 1f);
                Kanonenrohr.RenderMaterial.Diffuse = new Vector4(1, 1, 1, 1);
                scene.Add(Kanonenrohr);                
                ModelObject Kanonenhalterung = new ModelObject(new Vector3(0 + 0.0231f, -1.5f - 0.57428f, -6.37743f), Quaternion.Identity, new Vector3(1, 1, 1), CollisionType.ExactMesh, " ", "Kanonenhalterung", 1f);
                Kanonenhalterung.RenderMaterial.Diffuse = new Vector4(1, 1, 1, 1);
                scene.Add(Kanonenhalterung);

                // Neuer Controller an der Position 0/0/0
                Controller Kanone = new Controller(new Vector3(0, 0, 0));
                scene.Add(Kanone);
                Kanone.Add(Kanonenrohr);
                Kanone.Add(Kanonenhalterung);

                RevoluteJoint revolute = new RevoluteJoint(Kanonenhalterung.Physics, Kanonenrohr.Physics, new Vector3(-1.0941f, 0.5789f, -1.3587f), Vector3.Right);
                revolute.Limit.IsActive = true;
                revolute.Limit.MinimumAngle = -MathHelper.Pi;
                revolute.Limit.MaximumAngle = 0;*/

                ModelObject Welt = new ModelObject(new Vector3(0, -1.5f, -5f), Quaternion.CreateFromYawPitchRoll(0, -1.57f, 0), new Vector3(1, 1, 1), CollisionType.ExactMesh, " ", "Weltneu", 0f);
                Welt.PhysicsMaterial.Bounciness = 0.2f;
                Welt.RenderMaterial.Diffuse = new Vector4(1, 1, 1, 1);
                scene.Add(Welt);
                Objektverwaltung.addToUmgebungsListe(Welt);
                  
                //Box als alternative
                //LoadBox(new Vector3(0, -1.5f, -5), new Vector3(46, 0.2f, 1), 0f);

                //Lädt Spielhintergrund
                LoadBackground("himmel");
            }
        }

        private void LoadBackground(string bildname)
        {
            RenderMaterial hintergrundsbild = new RenderMaterial();
            hintergrund = new BoxObject(pos,
                                        new Vector3(58, 12, 0),
                                        0f);


            hintergrundsbild.Texture = Core.Content.Load<Texture2D>(bildname);
            hintergrundsbild.Diffuse = Color.White.ToVector4();
            hintergrundsbild.EnableGlow = true;
            hintergrund.RenderMaterial = hintergrundsbild;

            scene.Add(hintergrund);
        }

        public void MoveBackground(float xVerschiebung, float yVerschiebung)
        {
            Vector3 verschiebung = new Vector3(pos.X - 2 * xVerschiebung, pos.Y + 2 * yVerschiebung, pos.Z);
            hintergrund.Position = verschiebung;

        }
 
        private BoxObject LoadBox(Vector3 position, Vector3 dimension, float masse)
        {
            //Erstellt ein Objekt in der Scene.
            BoxObject box = new BoxObject(position,             //Position
                               dimension,                          //Kantenlängen
                               masse);
            scene.Add(box);

            return box;
            
        }

        private Scene scene;
        private Levels level;
    }
}
