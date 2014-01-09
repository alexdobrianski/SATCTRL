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
        ///////////////////////////////////////////////////////////////////////
        // space track retort 3 was source -> converted to c -> converted to C#
        ///////////////////////////////////////////////////////////////////////
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
        double XMNPDA = 24.0 * 60.0;

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
        DateTime ThatTime;
        protected void ConvertJulianDayToDateAndTime(double JulianDay)
        {
            long daysfrom2000 = (long)(JulianDay - 2451544.5);
            double flInDay = (JulianDay - 2451544.5) - (double)daysfrom2000;
            long iYear = 0;
            daysfrom2000 += 1; // 1Jan must be 1;
            while (daysfrom2000 > 366)
            {
                switch (iYear)
                {
                    case 24: daysfrom2000 -= 366; break;
                    case 23: daysfrom2000 -= 365; break;
                    case 22: daysfrom2000 -= 365; break;
                    case 21: daysfrom2000 -= 365; break;
                    case 20: daysfrom2000 -= 366; break;
                    case 19: daysfrom2000 -= 365; break;
                    case 18: daysfrom2000 -= 365; break;
                    case 17: daysfrom2000 -= 365; break;
                    case 16: daysfrom2000 -= 366; break;
                    case 15: daysfrom2000 -= 365; break;
                    case 14: daysfrom2000 -= 365; break;
                    case 13: daysfrom2000 -= 365; break;
                    case 12: daysfrom2000 -= 366; break;
                    case 11: daysfrom2000 -= 365; break;
                    case 10: daysfrom2000 -= 365; break;
                    case 9: daysfrom2000 -= 365; break;
                    case 8: daysfrom2000 -= 366; break;
                    case 7: daysfrom2000 -= 365; break;
                    case 6: daysfrom2000 -= 365; break;
                    case 5: daysfrom2000 -= 365; break;
                    case 4: daysfrom2000 -= 366; break;
                    case 3: daysfrom2000 -= 365; break;
                    case 2: daysfrom2000 -= 365; break;
                    case 1: daysfrom2000 -= 365; break;
                    case 0: daysfrom2000 -= 366; break;
                }
                iYear++;
            }
            long ThatTimewYear = iYear + 2000;
            long iMonth = 1;
            long iComp=0;
            long iDecr =0;
            while (1==1)
            {
                switch (iMonth)
                {
                    case 1: iComp = 31; iDecr = 31; break; // jan
                    case 2: if (iYear % 4 == 0) //leap year    //feb
                        {
                            iComp = 29; iDecr = 29;
                        }
                        else
                        {
                            iComp = 28; iDecr = 28;
                        }
                        break;
                    case 3: iComp = 31; iDecr = 31; break; // mar
                    case 4: iComp = 30; iDecr = 30; break; // apr
                    case 5: iComp = 31; iDecr = 31; break; // may
                    case 6: iComp = 30; iDecr = 30; break; // jun
                    case 7: iComp = 31; iDecr = 31; break; //jul
                    case 8: iComp = 31; iDecr = 31; break; // aug
                    case 9: iComp = 30; iDecr = 30; break; //sep
                    case 10: iComp = 31; iDecr = 31; break; // oct
                    case 11: iComp = 30; iDecr = 30; break; // nov
                }
                if (daysfrom2000 < iComp)
                    break;
                daysfrom2000 -= iDecr;
                if (++iMonth == 12) // what ?? getout!!!
                    break;
            }
            int iDay = (int)daysfrom2000;//+1;

            int iHour = (int)(flInDay * 24);
            int iMinutes = (int)(((flInDay - ((double)iHour) / 24.0)) * (24.0 * 60.0));
            int iSec = (int)(((flInDay - ((double)iHour) / 24.0) - ((double)iMinutes) / (24.0 * 60.0)) * (24.0 * 60.0 * 60.0));
            int iMils = (int)((flInDay - ((double)iHour) / (24.0) - ((double)iMinutes) / (24.0 * 60.0) - ((double)iSec) / (24.0 * 60.0 * 60.0)) * (24.0 * 60.0 * 60.0 * 1000.0));
            int ThatTimewMonth = (int)iMonth;
            int ThatTimewDay = iDay;
            int ThatTimewHour = iHour;
            int ThatTimewMinute = iMinutes;
            int ThatTimewSecond = iSec;
            int ThatTimewMilliseconds = iMils;
            //int ThatTimewDayOfWeek = 0;
            ThatTime = new DateTime((int)ThatTimewYear, ThatTimewMonth, ThatTimewDay,
                ThatTimewHour, ThatTimewMinute, ThatTimewSecond, ThatTimewMilliseconds);
        }
        protected int iDayOfTheYearZeroBase(int iDay, int iMonth, int iYear)
        {
            int iDays = iDay - 1;
            for (int i = iMonth - 1; i > 0; i--)
            {
                switch (i)
                {
                    case 11:// november
                        iDays += 30;break;
                    case 10:// october
                        iDays += 31;break;
                    case 9:// september
                        iDays += 30;break;
                    case 8://august
                        iDays += 31;break;
                    case 7:// july
                        iDays += 31;break;
                    case 6:// june
                        iDays += 30;break;
                    case 5:// may
                        iDays += 31;break;
                    case 4:// april
                        iDays += 30;break;
                    case 3:// march
                        iDays += 31;break;
                    case 2:// february
                        if ((iYear % 4) == 0) // leap year
                            iDays += 29;
                        else
                            iDays += 28;
                        break;
                    case 1: iDays += 31; // january
                        break;
                    case 0: iDays += 0;
                        break;
                }
            }
            //switch (iMonth - 1)
            //{
            //    case 11:// november
            //        iDays += 30;
            //    case 10:// october
            //        iDays += 31;
            //    case 9:// september
            //        iDays += 30;
            //    case 8://august
            //        iDays += 31;
            //    case 7:// july
            //        iDays += 31;
            //    case 6:// june
            //        iDays += 30;
            //    case 5:// may
            //        iDays += 31;
            //    case 4:// april
            //        iDays += 30;
            //    case 3:// march
            //        iDays += 31;
            //    case 2:// february
            //        if ((iYear % 4) == 0) // leap year
            //            iDays += 29;
            //        else
            //            iDays += 28;
            //    case 1: iDays += 31; // january
            //    case 0: iDays += 0;
            //        break;
            //}
            return iDays;
        }

        protected double ConvertDateTimeToTLEEpoch(int iDay, int iMonth, int iYear, int iHour, int iMin, int iSec, int iMills)
        {
            // An epoch of 98001.00000000 corresponds to 0000 UT on 1998 January 01—in other words, 
            // midnight between 1997 December 31 and 1998 January 01. 
            // An epoch of 98000.00000000 would actually correspond to the beginning of 1997 December 31—strange as that might seem. 
            // Note that the epoch day starts at UT midnight (not noon) and that all times are measured mean solar rather than sidereal time units.
            int mYear = iYear-2000;
            int mDays = iDayOfTheYearZeroBase(iDay, iMonth, iYear)+1;
	        long mCurSec = iHour * 60*60;
            mCurSec += iMin *60;
            mCurSec += iSec;
	        double dEpoch = mYear *1000.0 + mDays;
	        dEpoch += (((double)mCurSec)+ ((double)iMills/1000.0))/ (24.0*60.0*60.0);
            return dEpoch;
        }

        protected double ConverEpochDate2JulianDay(double KeplerDate)
        {
            // TLE elements is 1 day based - needs to minus at the end one day
            int iYear = (int)KeplerDate /1000;
            // date as it is = 2000/01/01     2451544.5, 2451910.5, 2452275.5, 2452640.5, 2453005.5, 2453371.5, 2453736.5, 2013-2456293.5
            // 
            double t2000_01_01_01 = 2451544.5;
            switch (iYear)
            {
                case 0: t2000_01_01_01 += 1; break;
            }
            for (int i = iYear; i > 0; i--)
            {
                if ((i - 1) % 4 == 0) 
                    t2000_01_01_01 += 366; 
                else 
                    t2000_01_01_01 += 365;
            }
            //switch(iYear)
            //{
            //    // add years = if you still alive !!! or just put formula if ((iYear-1)%4 == 0) t2000_01_01_01+=366; else t2000_01_01_01+=365;
            //    case 24: t2000_01_01_01 += 365;
            //    case 23:t2000_01_01_01+=365;
            //    case 22:t2000_01_01_01+=365;
            //    case 21:t2000_01_01_01+=366;
            //    case 20:t2000_01_01_01+=365;
            //    case 19:t2000_01_01_01+=365;
            //    case 18:t2000_01_01_01+=365;
            //    case 17:t2000_01_01_01+=366;
            //    case 16:t2000_01_01_01+=365;
            //    case 15:t2000_01_01_01+=365;
            //    case 14:t2000_01_01_01+=365;
            //    case 13:t2000_01_01_01+=366;
            //    case 12:t2000_01_01_01+=365;
            //    case 11:t2000_01_01_01+=365;
            //    case 10:t2000_01_01_01+=365;
            //    case  9:t2000_01_01_01+=366;
            //    case  8:t2000_01_01_01+=365;
            //    case  7:t2000_01_01_01+=365;
            //    case  6:t2000_01_01_01+=365;
            //    case  5:t2000_01_01_01+=366;
            //    case  4:t2000_01_01_01+=365;
            //    case  3:t2000_01_01_01+=365;
            //    case  2:t2000_01_01_01+=365;
            //    case  1:t2000_01_01_01+=366;
            //    case  0:;
            //// minus years = add if you interesting in anything from last century or use formala!!
            //}
            //long it2000_01_01_01 = t2000_01_01_01;
        //double RestOfTheDay = t2000_01_01_01 - (double)it2000_01_01_01;
            return t2000_01_01_01// - RestOfTheDay 
                    + KeplerDate - ((double)(iYear*1000))
                    -1; // epoch date is 1== 1 Jan - needs to adjust date.
        }

        protected double COPYKEPLER(String b1, int c1)
        {
            double a1;
            String szA1 = b1.Substring(0, c1);

            String szTempo = szA1.Substring(3);
            String szTempo0 = szA1.Substring(0, 1);
            String szTempo1 = szA1.Substring(1, 1);
            String szTempo2 = szA1.Substring(2, 1);
            if (szTempo0 == " ")
            {
                szTempo0 = "0";
                if (szTempo1 == " ")
                {
                    szTempo1 = "0";
                    if (szTempo2 == " ")
                    {
                        szTempo2 = "0";
                    }
                }
            }
            szTempo = szTempo0 + szTempo1 + szTempo2 + szTempo;
            a1 = Convert.ToDouble(szTempo);
            return a1;
        }
        double ProbEpoch;
        double ProbEpochS;
        double ProbFirstDervMeanMotion;
        double ProbSecondDervmeanMotion;
        double ProbDragterm;
	    int ProbElementSetType;
	    double ProbIncl;
	    double ProbAscNode;
        double ProbEcc;
        double ProbArgPer;
        double ProbMeanAnom;
        double ProbTPeriod;
        double ProbMeanMotion;
        double ProbTDays;
        double ProbTSec;
        double ProbRevAtEpoch;

        
        protected void ProcessTLE(string TLE2, string TLE3)
        {
            //#define COPYKEPLER(a1,b1,c1) memset(szTempo, 0, sizeof(szTempo)); memcpy(szTempo, b1, c1);  if (szTempo[0] == ' ') {szTempo[0] = '0';if (szTempo[1] == ' ') {szTempo[1] ='0';if (szTempo[2] == ' '){szTempo[2] ='0';}}}a1 = atof(szTempo);

            int iYear;
            int iDays;
            double dflTemp;
            //strcpy(Sat.Kepler1[0], szProbKeplerLine1);
            //strcpy(Sat.Kepler2[0], szProbKeplerLine2);
            //strcpy(Sat.Kepler3[0], szProbKeplerLine3);
	        //0         1         2         3         4         5         6         7
			//01234567890123456789012345678901234567890123456789012345678901234567890
            //1 25544U 98067A   04236.56031392  .00020137  00000-0  16538-3 0  5135\
            //2 25544  51.6335 341.7760 0007976 126.2523 325.9359 15.70406856328903"
            // format readings from FORTRAN:
            // 702 FORMAT(18X,D14.8,1X,F10.8,2(1X,F6.5,I2),/,7X,2(1X,F8.4),1X,F7.7,2(1X,F8.4),1X,F11.8)
            //
		    // "04236.56031392" Element Set Epoch (UTC) D14.8
			//  04                   - year
			//    236.56031392       - day
            //Sat.ProbEpoch[Sat.Elem] = atof(&Sat.Kepler2[Sat.Elem][18]);
            string My = TLE2.Substring(18);My = My.Substring(0, My.IndexOf(' '));
            
            ProbEpoch = Convert.ToDouble(My);

            //Sat.ProbEpochS[Sat.Elem] = Sat.ProbEpoch[Sat.Elem] =ConverEpochDate2JulianDay(Sat.ProbEpoch[Sat.Elem]);
            ProbEpochS = ProbEpoch =ConverEpochDate2JulianDay(ProbEpoch);
			ProbEpochS *=  60*60*24;
            // now for initial step asign emulation starting time:
			// if nothing is set then starting point is a last satellite epoch
            //if (dStartJD == 0.0)
            //{
            //    dStartJD = ConverEpochDate2JulianDay(Sat.ProbEpoch[Sat.Elem]);
            //}
			//  "_.00020137"      1st Derivative of the Mean Motion with respect to Time F10.8
			//double ProbFirstDervMeanMotion; XNDT2O
			ProbFirstDervMeanMotion = COPYKEPLER(TLE2.Substring(33),10);
			// "_00000-0"        2nd Derivative of the Mean Motion with respect to Time (decimal point assumed) 1X,F6.5,I2
			//double ProbSecondDervmeanMotion;,XNDD6O,IEXP
			String szTempo = ""; 
            szTempo += TLE2.Substring(44,1); if (szTempo==" ") szTempo = "0";
			szTempo += "."; 
            szTempo += TLE2.Substring(45, 5);
            ProbSecondDervmeanMotion = Convert.ToDouble(szTempo);
			if (TLE2.Substring(50,1) == "-")
				ProbSecondDervmeanMotion *= Math.Pow(10.0, - Convert.ToDouble(TLE2.Substring(51,1)));
			else //if (Sat.Kepler2[Sat.Elem][50] == '+') //is it corect ??
				ProbSecondDervmeanMotion *= Math.Pow(10.0, Convert.ToDouble(TLE2.Substring(51,1)));
			//// "_16538-3"        B* Drag Term 1X,F6.5,I2
			//double ProbDragterm; ,BSTAR,IBEXP
			szTempo = ""; 
            szTempo += TLE2.Substring(53,1); if (szTempo==" ") szTempo = "0";
			szTempo +=  "."; 
            szTempo += TLE2.Substring(54, 5);
            ProbDragterm = Convert.ToDouble(szTempo);
			if (TLE2.Substring(59,1) == "-")
				ProbDragterm *= Math.Pow(10.0, - Convert.ToDouble(TLE2.Substring(60,1)));
			// "0"              Element Set Type
			ProbElementSetType = Convert.ToInt32(TLE2.Substring(62,1));
			// "_513"            Element Number
			// "5"              Checksum
			//                        The checksum is the sum of all of the character in the data line, modulo 10. 
			//                        In this formula, the following non-numeric characters are assigned the indicated values: 
			//                        Blanks, periods, letters, '+' signs -> 0
			//                        '-' signs -> 1
			// "2"             Line Number
			// "25544"         Object Identification Number
			// "_51.6335"       Orbit Inclination (degrees) 1X,F8.4
			ProbIncl = COPYKEPLER(TLE3.Substring(8),8);
            ProbIncl = Math.PI * ProbIncl/180.0;
			// "341.7760"      Right Ascension of Ascending Node (degrees) 1X,F8.4
			ProbAscNode = COPYKEPLER(TLE3.Substring(17),8);
			ProbAscNode = Math.PI * ProbAscNode/180.0;
			// "0007976"       Eccentricity (decimal point assumed) F7.7
            szTempo = "."; 
            szTempo += TLE3.Substring(26, 7);
            ProbEcc = Convert.ToDouble(szTempo);
			// "126.2523"      Argument of Perigee (degrees) 1X,F8.4
			ProbArgPer = COPYKEPLER(TLE3.Substring(34),8);
			ProbArgPer = Math.PI * ProbArgPer/180.0;
			// "325.9359"      Mean Anomaly (degrees) 1X,F8.4
			ProbMeanAnom = COPYKEPLER(TLE3.Substring(43),8);
			ProbMeanAnom = Math.PI * ProbMeanAnom/180.0;
			// "15.70406856"    Mean Motion (revolutions/day)F11.8
			ProbTPeriod = COPYKEPLER(TLE3.Substring(52),11);
			ProbMeanMotion = ProbTPeriod;
			
            
            ProbTDays = 1.0/ProbTPeriod;
            ProbTSec = ProbTDays * 24.0 * 60.0 * 60.0;
			// "328903"        Revolution Number at Epoch
			ProbRevAtEpoch = COPYKEPLER(TLE3.Substring(63),6);
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

        private void SetType(int Itype)
        {
            bool TypeTLE = true;
            bool TypeOrbit = false;
            bool typeXYZ = false;
            if (Itype == 0)
            {
                TypeTLE = true;
                TypeOrbit = false;
                typeXYZ = false;
                ButtonConvertXYZ.Enabled = true;
                ButtonToOrbital.Enabled = true;
                ButtonToTLE.Enabled = false;
            }
            else if (Itype == 1)
            {
                TypeTLE = false;
                TypeOrbit = true;
                typeXYZ = false;
                ButtonConvertXYZ.Enabled = true;
                ButtonToOrbital.Enabled = false;
                ButtonToTLE.Enabled = true;

            }
            else if (Itype == 2)
            {
                TypeTLE = false;
                TypeOrbit = false;
                typeXYZ = true;
                ButtonConvertXYZ.Enabled = false;
                ButtonToOrbital.Enabled = true;
                ButtonToTLE.Enabled = true;
            }
            RadioButtonTLE.Checked = TypeTLE;
            RadioButtonOrbita.Checked = TypeOrbit;
            RadioButtonXYZ.Checked = typeXYZ;


            ButtonSetTLE.Enabled = TypeTLE;
            ButtonRestoreTLE.Enabled = TypeTLE;
            ButtonSetOrbital.Enabled = TypeOrbit;
            ButtonRestoreOrbital.Enabled = TypeOrbit;
            ButtonSetXYZ.Enabled = typeXYZ;
            ButtonRestoreXYZ.Enabled = typeXYZ;


            TextBoxTLE1.Enabled = TypeTLE;
            TextBoxTLE2.Enabled = TypeTLE;
            TextBoxOrbitInc.Enabled = TypeOrbit;
            TextBoxOrbitPerigee.Enabled = TypeOrbit;
            TextBoxApogee.Enabled = TypeOrbit;
            TextBoxOrbitPeriod.Enabled = TypeOrbit;
            TextBoxOrbitPereegeTime.Enabled = TypeOrbit;
            TextBoxOrbitLongitude.Enabled = TypeOrbit;
            TextBoxX.Enabled = typeXYZ;
            TextBoxY.Enabled = typeXYZ;
            TextBoxZ.Enabled = typeXYZ;
            TextBoxVx.Enabled = typeXYZ;
            TextBoxVy.Enabled = typeXYZ;
            TextBoxVz.Enabled = typeXYZ;

        }


        public long intMaxSessionN;
        double dX;
        double dY;
        string TraStatus;
        string szUsername =  "Main" ;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            SetType(0);
            if (Page.User.Identity.IsAuthenticated)
            {
                szUsername = Page.User.Identity.Name.ToString();
            }
            LabelUserName.Text = szUsername;

            string strMaxSessionN = HttpContext.Current.Application["strMaxSessionN"].ToString();
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
            object IsList = HttpContext.Current.Application["TargetLongitude" + szUsername];
            if (IsList != null)
            {
                string strLongitude = HttpContext.Current.Application["TargetLongitude" + szUsername].ToString();

                string strEW = strLongitude.Substring(0, 1);
                if (strEW == "W")
                    dX = -Convert.ToDouble(strLongitude.Substring(1));
                else
                    dX = Convert.ToDouble(strLongitude.Substring(1));
            }
            else
            {
            }
            IsList = HttpContext.Current.Application["TargetLatitude" + szUsername];
            if (IsList != null)
            {
                string strLatitude = HttpContext.Current.Application["TargetLatitude" + szUsername].ToString();
                string strNS = strLatitude.Substring(0, 1);
                if (strNS == "N")
                    dX = -Convert.ToDouble(strLatitude.Substring(1));
                else
                    dX = Convert.ToDouble(strLatitude.Substring(1));
            }
            else
            {
            }

            HttpContext.Current.Application["strPageUsed" + szUsername] = "TraCalc";
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
            object IsIt = HttpContext.Current.Application["InitKeplerLine1" + szUsername];
            if (IsIt == null)
            {
                String xml = null;
                String NameFile = "InitOrbit" + szUsername + ".xml";
                String MapPath = Server.MapPath(NameFile);
                int iDirAccound = MapPath.IndexOf("\\SatCtrl\\");
                if (iDirAccound > 0) // it is dir "account"
                {
                    MapPath = MapPath.Substring(0, iDirAccound);
                    MapPath += "\\SatCtrl\\\\SatCtrl\\\\"+ NameFile;
                }

                try
                {
                    xml = File.ReadAllText(MapPath);
                }
                catch (Exception Exs)
                {
                    xml = null;
                }
                if (xml == null) // init file is absent - use main for now
                {
                    NameFile = "InitOrbit" + "Main.xml";
                    MapPath = Server.MapPath(NameFile);
                    iDirAccound = MapPath.IndexOf("\\SatCtrl\\");
                    if (iDirAccound > 0) // it is dir "account"
                    {
                        MapPath = MapPath.Substring(0, iDirAccound);
                        MapPath += "\\SatCtrl\\\\SatCtrl\\\\" + NameFile;
                    }

                    try
                    {
                        xml = File.ReadAllText(MapPath);
                    }
                    catch (Exception Exs)
                    {
                        xml = null;
                    }
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

        protected void CheckBoxCurTime_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxCurTime.Checked)
            {
                TextBoxTimeCalc.Enabled = false;
                DateTime d = new DateTime();
                d = DateTime.UtcNow;
                String str_d_time = d.ToString("yy/MM/dd HH:mm:ss") + "." + d.Millisecond.ToString().PadLeft(3, '0');
                TextBoxTimeCalc.Text = str_d_time;
            }
            else
                TextBoxTimeCalc.Enabled = true;
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SetType(0);
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SetType(1);
        }

        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SetType(2);
        }

        protected void ButtonConvertXYZ_Click(object sender, EventArgs e)
        {
            DateTime d;
            if (CheckBoxCurTime.Checked)
            {
                TextBoxTimeCalc.Enabled = false;
                d = new DateTime();
                d = DateTime.UtcNow;
                String str_d_time = d.ToString("yy/MM/dd HH:mm:ss") + "." + d.Millisecond.ToString().PadLeft(3, '0');
                TextBoxTimeCalc.Text = str_d_time;

                ProcessTLE(TextBoxTLE1.Text.ToString(),TextBoxTLE2.Text.ToString());
			    double XKMPER = 6378.1350; //XKMPER kilometers/Earth radii 6378.135
                double AE = 1.0;
                double TEMP=2*Math.PI/XMNPDA/XMNPDA; // 2*pi / (1440 **2)
                double XNO=ProbMeanMotion*TEMP*XMNPDA; // rotation per day * 2*pi /1440 == rotation per day on 1 unit (1 min)
			    double XNDT2O=ProbFirstDervMeanMotion*TEMP;
			    double XNDD6O=ProbSecondDervmeanMotion*TEMP/XMNPDA;
                double BSTAR=ProbDragterm/AE;
                double dDate =ConvertDateTimeToTLEEpoch(d.Day, d.Month, d.Year, d.Hour, d.Minute, d.Second, d.Millisecond);

                SGP4((dDate - ProbEpoch)*XMNPDA, XNDT2O,XNDD6O,BSTAR,ProbIncl, ProbAscNode,ProbEcc, 
                     ProbArgPer, ProbMeanAnom,XNO);
			    double tProbX=X*XKMPER/AE*1000.0;
			    double tProbY=Y*XKMPER/AE*1000.0;
			    double tProbZ=Z*XKMPER/AE*1000.0;
			    double tProbVX=XDOT*XKMPER/AE*XMNPDA/86400.0*1000.0;
			    double tProbVY=YDOT*XKMPER/AE*XMNPDA/86400.0*1000.0;
			    double tProbVZ=ZDOT*XKMPER/AE*XMNPDA/86400.0*1000.0;
                TextBoxX.Text = Convert.ToString(tProbX);
                TextBoxY.Text = Convert.ToString(tProbY);
                TextBoxZ.Text = Convert.ToString(tProbZ);

                TextBoxVx.Text = Convert.ToString(tProbVX);
                TextBoxVy.Text = Convert.ToString(tProbVY);
                TextBoxVz.Text = Convert.ToString(tProbVZ);

            }

        }

        protected void ButtonToOrbital_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonSetTLE_Click(object sender, EventArgs e)
        {
            String SVal1 = szUsername;
            HttpContext.Current.Application["InitKeplerLine1" + szUsername] = SVal1;
            String SVal2 = TextBoxTLE1.Text.ToString();
            HttpContext.Current.Application["InitKeplerLine2" + szUsername] = SVal2;
            String SVal3 = TextBoxTLE2.Text.ToString();
            HttpContext.Current.Application["InitKeplerLine3" + szUsername] = SVal3;

        }

        protected void ButtonRestoreTLE_Click(object sender, EventArgs e)
        {
            string SVal1 = null;
            string SVal2 = null;
            string SVal3 = null;
            object IsIt = HttpContext.Current.Application["InitKeplerLine1" + szUsername];
            if (IsIt == null)
            {
            }
            else
            {
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
        }

        protected void ButtonSetOrbital_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonRestoreOrbital_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonSetXYZ_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonRestoreXYZ_Click(object sender, EventArgs e)
        {

        }

    }
}
