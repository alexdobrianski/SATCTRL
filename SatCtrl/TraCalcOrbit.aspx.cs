using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using System.Net.Cache;
using System.Net.Cache;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace SatCtrl
{
    public partial class _TraCalcOrbit : System.Web.UI.Page
    {
        // in FORTARN variable with a name begining with I is an integer variable 
        // The function subroutine FMOD2P is
        // passed an angle in radians and returns the angle in radians within the range of 0 to 2 PI.
        protected double FMOD2P(double X)
        {
	        // COMMON/C2/DE2RA,PI,PIO2,TWOPI,X3PIO2
	        //double DE2RA,PI,PIO2,TWOPI,X3PIO2;
	        double Rez_FMOD2P;
	        int I;
            Rez_FMOD2P = X;
	        //I=FMOD2P/TWOPI;
            I = (int)(Rez_FMOD2P / (2.0 * Math.PI));
	        //FMOD2P=FMOD2P-I*TWOPI;
            Rez_FMOD2P = Rez_FMOD2P - I * (2.0 * Math.PI);
	        //IF(FMOD2P.LT.0) FMOD2P=FMOD2P+TWOPI
            if (Rez_FMOD2P < 0.0)
                Rez_FMOD2P = Rez_FMOD2P + 2.0 * Math.PI;
            return Rez_FMOD2P;
        //RETURN
        //END
        }

        // strange function :The function subroutine ACTAN is passed the values of sine and cosine in that order and
        // it returns the angle in radians within the range of 0 to 2 pi
        protected double ACTAN(double SINX,double COSX)
        {
	        double ACTAN,TEMP;
	        //COMMON/C2/DE2RA,PI,PIO2,TWOPI,X3PIO2
	        double X3PIO2 = 3.0*Math.PI/2.0;
            double PIO2 = Math.PI / 2.0;
	        ACTAN=0.0;
	        if (COSX == 0.0) goto M_5;
	        if (COSX > 0.0) goto M_1;
            ACTAN = Math.PI;
	        goto M_7;
        M_1:
	        if (SINX == 0.0) goto M_8;
	        if (SINX > 0.0) goto M_7;
            ACTAN = 2.0 * Math.PI;
	        goto M_7;
        M_5:
	        if (SINX == 0.0) goto M_8;
	        if (SINX > 0.0) goto M_6;
	        ACTAN=X3PIO2;
	        goto M_8;
        M_6:
	        ACTAN=PIO2;
	        goto M_8;
        M_7:
	        TEMP=SINX/COSX;
	        ACTAN=ACTAN+Math.Atan(TEMP);
        M_8:
	        return ACTAN;
	        //RETURN
	        //END
    }

        // was based in SGP4 on G=398600.8 km**3/s**2
        double BIG_XKE = 0.743669161E-1;
        // that variables are simylation of the pointers on the parameters
        double X;
        double Y;
        double Z;
        double XDOT;
        double YDOT;
        double ZDOT;

        // space track retort 3 was source -> converted to c -> converted to C#
        // original, more acurate, takes account about drag == according Aksenov needs to use exact formulas from SGP4 to correctly process
        // data. Backwards formulas should applied before everything 
        // Read comments: (do) RECOVER ORIGINAL MEAN MOTION (XNODP) AND SEMIMAJOR AXIS (AODP) FROM INPUT ELEMENTS
        protected void SGP4(double TSINCE,/*double EPOCH,*/ 
          double nuXNDT2O, 
          double nuXNDD6O,/*IEXP,*/
          double BSTAR,/*IBEXP,*/
	      double XINCL,  // the ômeanö inclination at epoch
          double XNODEO, //the ômeanö longitude of ascending node at epoch
          double EO,     // the ômeanö eccentricity at epoch
          double OMEGAO, // the ômeanö argument of perigee at epoch
          double XMO,   // (M0) the ômeanö mean anomaly at epoch
          double XNO   // (k0) the SGP type ômeanö mean motion at epoch
		  )
        {
            double D2 = 0.0;
            double D3 = 0.0;
            double D4 = 0.0;
            double T3COF = 0.0;
            double T4COF = 0.0;
            double T5COF = 0.0;

            //COMMON/E1/XMO,XNODEO,OMEGAO,EO,XINCL,XNO,XNDT2O,
            //1 XNDD6O,BSTAR,X,Y,Z,XDOT,YDOT,ZDOT,EPOCH,DS50
            //COMMON/C1/CK2,CK4,E6A,QOMS2T,S,TOTHRD,
            //1 XJ3,XKE,XKMPER,XMNPDA,AE
	        //long double /*EPOCH,*/ DS50;
	        double XKE = BIG_XKE;//.743669161E-1;
	        double XKMPER = 6378.1350;
	        //IF (IFLAG .EQ. 0) GO TO 100
	        //* RECOVER ORIGINAL MEAN MOTION (XNODP) AND SEMIMAJOR AXIS (AODP)
	        //* FROM INPUT ELEMENTS
	        //A1=(XKE/XNO)**TOTHRD;
	        double XJ2 = 1.082616E-3; //the second gravitational zonal harmonic of the Earth
	        double XJ3 = -.253881E-5; // the third gravitational zonal harmonic of the Earth
	        double XJ4 = -1.65597E-6; // the fourth gravitational zonal harmonic of the Earth
	        double AE = 1.0;          // the equatorial radius of the Earth - actualy it is not true it is one == everything measuared in that radiuses
	        double QO =120.0;         // parameter for the SGP4/SGP8 density function
	        double SO = 78.0;         // parameter for the SGP4/SGP8 density function

	        double CK2=.5*XJ2*AE*AE;
	        //CK4=-.375*XJ4*AE**4
	        double CK4=-.375*XJ4*AE*AE*AE*AE;
	        double QOMS2T=Math.Pow(((QO-SO)*AE/XKMPER),(double)4.0);
	        double S=AE*(1.0+SO/XKMPER);

            //The original mean motion (n0") and semimajor axis (a0") are first recovered from the input elements by the equations:
            //
            // a1 = (k0 /n0) ** (2/3)
            //
            // b1 = 3/2 * k2/a1 * (3 * (cos(i0))**2 -1)/(1-e0**2)**(3/2)
            //
            // a0 = a1 * (1 - 1/3 b1 - b1**2  - 134/81 b1**3)
            //
            // b0 - 3/2 * k2/(a0**2) * (3 * (cos(i0))**2 - 1)/(1- e9**2)3/2
            //
            // n0" = n0/ (1+b0)
            // a0" = a0/(1-b0)
            //
	        //A1=(XKE/XNO)**TOTHRD;
	        double A1=Math.Pow((XKE/XNO),(double)2.0/(double)3.0);
	        double COSIO=Math.Cos(XINCL);
	        double THETA2=COSIO*COSIO;
	        double X3THM1=3.0*THETA2-1.0;
	        double EOSQ=EO*EO;
	        double BETAO2=1.0-EOSQ;
	        double BETAO=Math.Sqrt(BETAO2);
	        double DEL1=1.5*CK2*X3THM1/(A1*A1*BETAO*BETAO2);
	        //AO=A1*(1.-DEL1*(.5*TOTHRD+DEL1*(1.+134./81.*DEL1)))
	        double AO=A1*(1.0-DEL1*(.5*(2.0/3.0)+DEL1*(1.0+134.0/81.0*DEL1)));
	        double DELO=1.5*CK2*X3THM1/(AO*AO*BETAO*BETAO2);
            // ProbMeanMotion = XNO / (2*pi) * 1440.0
	        double XNODP=XNO/(1.0+DELO);
            // and this is a semimajor axis
	        double AODP=AO/(1.0-DELO);
	        //* INITIALIZATION
	        //* FOR PERIGEE LESS THAN 220 KILOMETERS, THE ISIMP FLAG IS SET AND
	        //* THE EQUATIONS ARE TRUNCATED TO LINEAR VARIATION IN SQRT A AND
	        //* QUADRATIC VARIATION IN MEAN ANOMALY. ALSO, THE C3 TERM, THE
	        //* DELTA OMEGA TERM, AND THE DELTA M TERM ARE DROPPED.
	        int ISIMP=0;
	        //IF((AODP*(1.-EO)/AE) .LT. (220./XKMPER+AE)) ISIMP=1
	        if((AODP*(1.0-EO)/AE) < (220.0/XKMPER+AE)) 
		        ISIMP=1;
	        //* FOR PERIGEE BELOW 156 KM, THE VALUES OF
	        //* S AND QOMS2T ARE ALTERED
	        double S4=S;
	        double QOMS24=QOMS2T;
	        double PERIGE=(AODP*(1.0-EO)-AE)*XKMPER;
	        //IF(PERIGE .GE. 156.) GO TO 10
	        if (PERIGE >= 156.0) 
		        goto M_10;
	        S4=PERIGE-78.0;
	        //IF(PERIGE .GT. 98.) GO TO 9
	        if (PERIGE >=98.0) 
		        goto M_9;
	        S4=20.0;
        M_9:
	        //QOMS24=((120.-S4)*AE/XKMPER)**4;
	        QOMS24=Math.Pow(((120.0-S4)*AE/XKMPER),4);
	        S4=S4/XKMPER+AE;
        M_16:
        M_10:
	        double PINVSQ=1.0/(AODP*AODP*BETAO2*BETAO2);
	        double TSI=1.0/(AODP-S4);
	        double ETA=AODP*EO*TSI;
	        double ETASQ=ETA*ETA;
	        double EETA=EO*ETA;
	        double PSISQ=Math.Abs(1.0-ETASQ);
	        //COEF=QOMS24*TSI**4
	        double COEF=QOMS24*TSI*TSI*TSI*TSI;
	        //COEF1=COEF/PSISQ**3.5;
	        double COEF1=COEF/Math.Pow(PSISQ,(double)3.5);

            double C2=COEF1*XNODP*(AODP*(1.0+1.5*ETASQ+EETA*(4.0+ETASQ))+.75* CK2*TSI/PSISQ*X3THM1*(8.0+3.0*ETASQ*(8.0+ETASQ)));
	        double C1=BSTAR*C2;
	        double SINIO=Math.Sin(XINCL);
	        //A3OVK2=-XJ3/CK2*AE**3;
	        double A3OVK2=-XJ3/CK2*(AE*AE*AE);
	        double C3=COEF*TSI*A3OVK2*XNODP*AE*SINIO/EO;
	        double X1MTH2=1.0-THETA2;
	        double C4=2.0*XNODP*COEF1*AODP*BETAO2*(ETA*(2.0+.5*ETASQ)+EO*(.5+2.0*ETASQ)-2.0*CK2*TSI/
    			(AODP*PSISQ)*(-3.0*X3THM1*(1.0-2.0*EETA+ETASQ* (1.5-.5*EETA))+.75*X1MTH2*(2.0*ETASQ-EETA* (1.0+ETASQ))*Math.Cos(2.0*OMEGAO)));
            double C5=2.0*COEF1*AODP*BETAO2*(1.0+2.75*(ETASQ+EETA)+EETA*ETASQ);
	        double THETA4=THETA2*THETA2;
	        double TEMP1=3.0*CK2*PINVSQ*XNODP;
	        double TEMP2=TEMP1*CK2*PINVSQ;
	        double TEMP3=1.25*CK4*PINVSQ*PINVSQ*XNODP;
	        double XMDOT=XNODP+.5*TEMP1*BETAO*X3THM1+.0625*TEMP2*BETAO* (13.0-78.0*THETA2+137.0*THETA4);
	        double X1M5TH=1.0-5.0*THETA2;
	        double OMGDOT=-.5*TEMP1*X1M5TH+.0625*TEMP2*(7.0-114.0*THETA2+ 395.0*THETA4)+TEMP3*(3.0-36.0*THETA2+49.0*THETA4);
	        double XHDOT1=-TEMP1*COSIO;
	        double XNODOT=XHDOT1+(.5*TEMP2*(4.0-19.0*THETA2)+2.0*TEMP3*(3.0-7.0*THETA2))*COSIO;
	        double OMGCOF=BSTAR*C3*Math.Cos(OMEGAO);
	        double XMCOF=-(2.0/3.0)*COEF*BSTAR*AE/EETA;
	        double XNODCF=3.5*BETAO2*XHDOT1*C1;
	        double T2COF=1.5*C1;
	        double XLCOF=.125*A3OVK2*SINIO*(3.0+5.0*COSIO)/(1.0+COSIO);
	        double AYCOF=.25*A3OVK2*SINIO;
	        //DELMO=(1.+ETA*COS(XMO))**3
	        double DELMO=Math.Pow((1.0+ETA*Math.Cos(XMO)),3);
	        double SINMO=Math.Sin(XMO);
	        double X7THM1=7.0*THETA2-1.0;
	        // IF(ISIMP .EQ. 1) GO TO 90
	        if (ISIMP == 1) 
		        goto M_90;
	        double C1SQ=C1*C1;
	        D2 = 4.0 * AODP * TSI * C1SQ;
	        double TEMP=D2*TSI*C1/3.0;
	        D3=(17.0*AODP+S4)*TEMP;
	        D4=.5*TEMP*AODP*TSI*(221.0*AODP+31.0*S4)*C1;
        M_17:
	        T3COF=D2+2.0*C1SQ;
	        T4COF=.25*(3.0*D3+C1*(12.0*D2+10.0*C1SQ));
	        T5COF=.2*(3.0*D4+12.0*C1*D3+6.0*D2*D2+15.0*C1SQ*(2.0*D2+C1SQ));
        M_90:
	        //IFLAG=0;
	        //* UPDATE FOR SECULAR GRAVITY AND ATMOSPHERIC DRAG
        M_100:
	        double XMDF=XMO+XMDOT*TSINCE;
	        double OMGADF=OMEGAO+OMGDOT*TSINCE;
	        double XNODDF=XNODEO+XNODOT*TSINCE;
	        double OMEGA=OMGADF;
	        double XMP=XMDF;
	        double TSQ=TSINCE*TSINCE;
	        double XNODE=XNODDF+XNODCF*TSQ;
	        double TEMPA=1.0-C1*TSINCE;
	        double TEMPE=BSTAR*C4*TSINCE;
	        double TEMPL=T2COF*TSQ;
	        // IF(ISIMP .EQ. 1) GO TO 110
	        if (ISIMP == 1) 
		        goto M_110;
	        double DELOMG=OMGCOF*TSINCE;
	        // DELM=XMCOF*((1.+ETA*COS(XMDF))**3-DELMO)
	        double DELM=XMCOF*(Math.Pow((1.0+ETA*Math.Cos(XMDF)),3)-DELMO);
	        TEMP=DELOMG+DELM;
	        XMP=XMDF+TEMP;
	        OMEGA=OMGADF-TEMP;
	        double TCUBE=TSQ*TSINCE;
	        double TFOUR=TSINCE*TCUBE;
	        TEMPA=TEMPA-D2*TSQ-D3*TCUBE-D4*TFOUR;
	        TEMPE=TEMPE+BSTAR*C5*(Math.Sin(XMP)-SINMO);
	        TEMPL=TEMPL+T3COF*TCUBE+TFOUR*(T4COF+TSINCE*T5COF);
        M_110:
	        //A=AODP*TEMPA**2;
	        double A=AODP*TEMPA*TEMPA;
	        double E=EO-TEMPE;
	        double XL=XMP+OMEGA+XNODE+XNODP*TEMPL;
	        double BETA=Math.Sqrt(1.0-E*E);
	        //XN=XKE/A**1.5
	        double XN=XKE/Math.Pow(A,(double)1.5);
	        //* LONG PERIOD PERIODICS
	        double AXN=E*Math.Cos(OMEGA);
	        TEMP=1.0/(A*BETA*BETA);
	        double XLL=TEMP*XLCOF*AXN;
	        double AYNL=TEMP*AYCOF;
	        double XLT=XL+XLL;
	        double AYN=E*Math.Sin(OMEGA)+AYNL;
	        //* SOLVE KEPLERS EQUATION
	        double CAPU=FMOD2P(XLT-XNODE);
        M_18:
	        TEMP2=CAPU;
	        // DO 130 I=1,10
	        double SINEPW = 0.0;
	        double COSEPW = 0.0;
	        double TEMP4 = 0.0;
	        double TEMP5 = 0.0;
	        double TEMP6 = 0.0;
	        double EPW;
	        for (int i =1; i <=30; i++)
	        {
		        SINEPW=Math.Sin(TEMP2);
		        COSEPW=Math.Cos(TEMP2);
		        TEMP3=AXN*SINEPW;
		        TEMP4=AYN*COSEPW;
		        TEMP5=AXN*COSEPW;
		        TEMP6=AYN*SINEPW;
		        EPW=(CAPU-TEMP4+TEMP3-TEMP2)/(1.0-TEMP5-TEMP6)+TEMP2;
		        //IF(ABS(EPW-TEMP2) .LE. E6A) GO TO 140
		        if(Math.Abs(EPW-TEMP2) <= 1.0e-15) 
			        goto M_140;
            M_130:	TEMP2=EPW;
	        }

	        //* SHORT PERIOD PRELIMINARY QUANTITIES
            M_140:
	        double ECOSE=TEMP5+TEMP6;
	        double ESINE=TEMP3-TEMP4;
	        double ELSQ=AXN*AXN+AYN*AYN;
	        TEMP=1.0-ELSQ;
	        double PL=A*TEMP;
	        double R=A*(1.0-ECOSE);
	        TEMP1=1.0/R;
	        double RDOT=XKE*Math.Sqrt(A)*ESINE*TEMP1;
	        double RFDOT=XKE*Math.Sqrt(PL)*TEMP1;
	        TEMP2=A*TEMP1;
	        double BETAL=Math.Sqrt(TEMP);
	        TEMP3=1.0/(1.0+BETAL);
	        double COSU=TEMP2*(COSEPW-AXN+AYN*ESINE*TEMP3);
	        double SINU=TEMP2*(SINEPW-AYN-AXN*ESINE*TEMP3);
	        double U=ACTAN(SINU,COSU);
	        double SIN2U=2.0*SINU*COSU;
	        double COS2U=2.0*COSU*COSU-1.0;
	        TEMP=1.0/PL;
	        TEMP1=CK2*TEMP;
	        TEMP2=TEMP1*TEMP;
	        // * UPDATE FOR SHORT PERIODICS
	        double RK=R*(1.0-1.5*TEMP2*BETAL*X3THM1)+.5*TEMP1*X1MTH2*COS2U;
	        double UK=U-.25*TEMP2*X7THM1*SIN2U;
	        double XNODEK=XNODE+1.5*TEMP2*COSIO*SIN2U;
	        double XINCK=XINCL+1.5*TEMP2*COSIO*SINIO*COS2U;
	        double RDOTK=RDOT-XN*TEMP1*X1MTH2*SIN2U;
	        double RFDOTK=RFDOT+XN*TEMP1*(X1MTH2*COS2U+1.5*X3THM1);
	        //* ORIENTATION VECTORS
	        double SINUK=Math.Sin(UK);
            double COSUK = Math.Cos(UK);
        M_19:
            double SINIK = Math.Sin(XINCK);
        double COSIK = Math.Cos(XINCK);
        double SINNOK = Math.Sin(XNODEK);
        double COSNOK = Math.Cos(XNODEK);
	        double XMX=-SINNOK*COSIK;
	        double XMY=COSNOK*COSIK;
	        double UX=XMX*SINUK+COSNOK*COSUK;
	        double UY=XMY*SINUK+SINNOK*COSUK;
	        double UZ=SINIK*SINUK;
	        double VX=XMX*COSUK-COSNOK*SINUK;
	        double VY=XMY*COSUK-SINNOK*SINUK;
	        double VZ=SINIK*COSUK;
	        //* POSITION AND VELOCITY
	        X=RK*UX;
	        Y=RK*UY;
	        Z=RK*UZ;
	        XDOT=RDOTK*UX+RFDOTK*VX;
	        YDOT=RDOTK*UY+RFDOTK*VY;
	        ZDOT=RDOTK*UZ+RFDOTK*VZ;
        //RETURN
        //END
        }


        protected String GetValue(String xml, String SearchStr, int iInstance)
        {
            String MySearch = "\"" + SearchStr + "\"";
            int FirstLine = xml.IndexOf(MySearch);
            int iCount = 0;

            if (FirstLine > 0)
            {
                do
                {
                    if (iCount == iInstance)
                    {
                        int FirstValue = xml.IndexOf("value=", FirstLine) + 7;
                        int LastValue = xml.IndexOf('\"', FirstValue) - 1;
                        return xml.Substring(FirstValue, LastValue - FirstValue + 1);
                    }
                    FirstLine = xml.IndexOf(MySearch, FirstLine + 1);
                    iCount += 1;
                }
                while (FirstLine > 0);
            }
            return null;
        }
        public long intMaxSessionN;
        double dX;
        double dY;
        string TraStatus;
        string szUsername =  "Main" ;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string strMaxSessionN = HttpContext.Current.Application["strMaxSessionN"].ToString();
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
            object IsList = HttpContext.Current.Application["TargetLongitude"];
            if (IsList != null)
            {
                string strLongitude = HttpContext.Current.Application["TargetLongitude"].ToString();

                string strEW = strLongitude.Substring(0, 1);
                if (strEW == "W")
                    dX = -Convert.ToDouble(strLongitude.Substring(1));
                else
                    dX = Convert.ToDouble(strLongitude.Substring(1));
            }
            else
            {
            }
            IsList = HttpContext.Current.Application["TargetLatitude"];
            if (IsList != null)
            {
                string strLatitude = HttpContext.Current.Application["TargetLatitude"].ToString();
                string strNS = strLatitude.Substring(0, 1);
                if (strNS == "N")
                    dX = -Convert.ToDouble(strLatitude.Substring(1));
                else
                    dX = Convert.ToDouble(strLatitude.Substring(1));
            }
            else
            {
            }
            
            HttpContext.Current.Application["strPageUsed"] = "TraCalc";
            // number of distributed nodes
            //LabelNodes.Text = "0";
            // status of distributed calculations
            TraStatus = "non avalable";
            //LabelStatus.Text = TraStatus;
            //HttpContext.Current.Application["TraDistStatus"] = TraStatus;
            if (TraStatus == "non avalable")
            {
            //    ButtonImpOptimization.Enabled = false;
            //    ButtonFindImp.Enabled = false;
            }

            string SVal1 = null;
            string SVal2 = null;
            string SVal3 = null;
            object IsIt = HttpContext.Current.Application["InitOrbitKeplerLine1" + szUsername];
            if (IsIt == null)
            {
                String xml = null;
                String MapPath = Server.MapPath("InitOrbit.xml");
                try
                {
                    xml = File.ReadAllText(MapPath);
                }
                catch (Exception Exs)
                {
                    xml = null;
                }
                if (xml != null)
                {
                    SVal1 = GetValue(xml, "ProbKeplerLine1", 0);
                    HttpContext.Current.Application["InitKeplerLine1" + szUsername] = SVal1;
                    SVal2 = GetValue(xml, "ProbKeplerLine2", 0);
                    HttpContext.Current.Application["InitKeplerLine2" + szUsername] = SVal2;
                    SVal3 = GetValue(xml, "ProbKeplerLine3", 0);
                    HttpContext.Current.Application["InitKeplerLine3" + szUsername] = SVal3;
                }
            }
            IsIt = HttpContext.Current.Application["InitKeplerLine1" + szUsername];
            object IsIt2 = HttpContext.Current.Application["InitKeplerLine2" + szUsername];
            object IsIt3 = HttpContext.Current.Application["InitKeplerLine3" + szUsername];
            if ((IsIt != null) && (IsIt2 != null) && (IsIt3 != null))
            {
                SVal1 = IsIt.ToString();
                SVal2 = IsIt2.ToString();
                SVal3 = IsIt3.ToString();
                TextBoxTLE1.Text = SVal2.ToString(); TextBoxTLE2.Text = SVal3.ToString();
            }

        }
        protected String AddHexString(String Str2)
        {
            String Str1 = "";
            for (int i = 0; i < Str2.Length; i++)
            {
                Char CharS = Str2.ElementAt(i);
                if ((CharS >= '0') && ((CharS <= '9')) || (CharS >= 'a') && ((CharS <= 'z')) || (CharS >= 'A') && ((CharS <= 'Z')))
                {
                    Str1 += CharS;
                }
                else
                {
                    Str1 = Str1 + "%" + Convert.ToByte(CharS).ToString("x2");
                }
            }
            return Str1;
        }

    }
}
